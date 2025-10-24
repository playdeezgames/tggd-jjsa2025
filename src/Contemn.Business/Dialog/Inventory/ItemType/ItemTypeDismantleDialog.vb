Imports TGGD.Business

Friend Class ItemTypeDismantleDialog
    Inherits CharacterDialog

    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            character,
            Function(x) GenerateCaption(x, itemType),
            Function(x) GenerateChoices(x, itemType),
            Function(x) GenerateLines(x, itemType),
            ItemTypeDialog.LaunchMenu(character, itemType))
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, $"You have {character.GetCountOfItemType(itemType)}")
            }
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of IDialogChoice)
        Dim itemCount = character.GetCountOfItemType(itemType)
        Dim result As New List(Of IDialogChoice) From {
            New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT),
            New DialogChoice(DISMANTLE_ONE_CHOICE, DISMANTLE_ONE_TEXT)
        }
        If itemCount \ 2 > 0 Then
            result.Add(New DialogChoice(DISMANTLE_HALF_CHOICE, $"{DISMANTLE_HALF_TEXT}({itemCount \ 2})"))
        End If
        If itemCount > 1 Then
            result.Add(New DialogChoice(DISMANTLE_ALL_CHOICE, DISMANTLE_ALL_TEXT))
        End If
        Return result
    End Function

    Private Shared Function GenerateCaption(character As ICharacter, itemType As String) As String
        Dim descriptor = ItemTypes.Descriptors(itemType)
        Return $"Dismantle {descriptor.ItemTypeName}..."
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case DISMANTLE_ALL_CHOICE
                Return DismantleAll()
            Case DISMANTLE_HALF_CHOICE
                Return DismantleHalf()
            Case DISMANTLE_ONE_CHOICE
                Return DismantleOne()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function DismantleOne() As IDialog
        Return Dismantle(1)
    End Function

    Private Function Dismantle(itemCount As Integer) As IDialog
        Dim descriptor = ItemTypes.Descriptors(itemType)
        Dim dialogLines As New List(Of IDialogLine) From
            {
                New DialogLine(MoodType.Info, $"-{itemCount} {descriptor.ItemTypeName}")
            }
        For Each item In character.ItemsOfType(itemType).Take(itemCount).ToList
            character.Dismantle(item)
        Next
        For Each entry In descriptor.DepletionTable.ToDictionary(Function(x) x.Key, Function(x) x.Value * itemCount)
            Dim entryDescriptor = ItemTypes.Descriptors(entry.Key)
            dialogLines.Add(New DialogLine(MoodType.Info, $"+{entry.Value} {entryDescriptor.ItemTypeName}"))
        Next
        Return New OkDialog(
            "Get So Dismantled!",
            dialogLines,
            LaunchMenu(character, itemType))
    End Function

    Private Function DismantleHalf() As IDialog
        Return Dismantle(character.GetCountOfItemType(itemType) \ 2)
    End Function

    Private Function DismantleAll() As IDialog
        Return Dismantle(character.GetCountOfItemType(itemType))
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter, itemType As String) As Func(Of IDialog)
        Return Function() If(
            character.HasItemsOfType(itemType),
            CType(New ItemTypeDismantleDialog(character, itemType), IDialog),
            InventoryDialog.LaunchMenu(character).Invoke)
    End Function
End Class
