Imports TGGD.Business

Friend Class ItemsOfTypeDialog
    Inherits CharacterDialog

    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            character,
            Function(x) ItemTypes.Descriptors(itemType).ItemTypeName,
            Function(x) GenerateChoices(x, itemType),
            Function(x) GenerateLines(x, itemType),
            InventoryDialog.LaunchMenu(character))
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, $"You have {character.GetCountOfItemType(itemType)}.")
            }
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        result.AddRange(character.ItemsOfType(itemType).Select(Function(x) New DialogChoice(x.ItemId.ToString, x.Name)))
        If character.GetCountOfItemType(itemType) > 1 Then
            result.Add(New DialogChoice(DROP_ALL_CHOICE, DROP_ALL_TEXT))
        End If
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case DROP_ALL_CHOICE
                Return DropAll()
            Case Else
                Return ItemOfType(CInt(choice))
        End Select
    End Function

    Private Function DropAll() As IDialog
        For Each item In character.ItemsOfType(itemType)
            character.RemoveItem(item)
            character.Location.AddItem(item)
        Next
        Return InventoryDialog.LaunchMenu(character).Invoke()
    End Function

    Private Function ItemOfType(itemId As Integer) As IDialog
        Return New ItemOfTypeDialog(character, character.World.GetItem(itemId))
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter, itemType As String) As Func(Of IDialog)
        Return Function() If(
            character.HasItemsOfType(itemType),
            New ItemsOfTypeDialog(character, itemType),
            InventoryDialog.LaunchMenu(character).Invoke())
    End Function
End Class
