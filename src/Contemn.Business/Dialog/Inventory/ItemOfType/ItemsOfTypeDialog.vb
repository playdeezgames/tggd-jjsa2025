Imports TGGD.Business

Friend Class ItemsOfTypeDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            ItemTypes.Descriptors(itemType).ItemTypeName,
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
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
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return ItemOfType(CInt(choice))
        End Select
    End Function

    Private Function ItemOfType(itemId As Integer) As IDialog
        Return New ItemOfTypeDialog(character, character.World.GetItem(itemId))
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return InventoryDialog.LaunchMenu(character).Invoke()
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter, itemType As String) As Func(Of IDialog)
        Return Function() If(
            character.HasItemsOfType(itemType),
            New ItemsOfTypeDialog(character, itemType),
            InventoryDialog.LaunchMenu(character).Invoke())
    End Function
End Class
