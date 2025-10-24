Imports TGGD.Business

Friend Class ItemOfTypeDialog
    Inherits CharacterDialog
    Shared ReadOnly DROP_CHOICE As String = NameOf(DROP_CHOICE)
    Const DROP_TEXT = "Drop"
    Shared ReadOnly DISMANTLE_CHOICE As String = NameOf(DISMANTLE_CHOICE)
    Const DISMANTLE_TEXT = "Dismantle"

    Private ReadOnly item As IItem

    Public Sub New(
                  character As ICharacter,
                  item As IItem)
        MyBase.New(
            character,
            Function(x) GenerateCaption(item),
            Function(x) GenerateChoices(x, item),
            Function(x) GenerateLines(item),
            ItemsOfTypeDialog.LaunchMenu(character, item.ItemType))
        Me.item = item
    End Sub

    Private Shared Function GenerateLines(item As IItem) As IEnumerable(Of IDialogLine)
        Return item.Describe()
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, item As IItem) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        result.AddRange(item.GetAvailableChoices(character))
        If item.CanDismantle Then
            result.Add(New DialogChoice(DISMANTLE_CHOICE, DISMANTLE_TEXT))
        End If
        result.Add(New DialogChoice(DROP_CHOICE, DROP_TEXT))
        Return result
    End Function

    Private Shared Function GenerateCaption(item As IItem) As String
        Return item.Name
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case DROP_CHOICE
                Return Drop()
            Case DISMANTLE_CHOICE
                Return Dismantle()
            Case Else
                Return item.MakeChoice(character, choice)
        End Select
    End Function

    Private Function Dismantle() As IDialog
        Dim descriptor = item.Descriptor
        Dim dialogLines As New List(Of IDialogLine) From
            {
                New DialogLine(MoodType.Info, $"-1 {descriptor.ItemTypeName}")
            }
        character.Dismantle(item)
        For Each entry In descriptor.DepletionTable
            Dim entryDescriptor = ItemTypes.Descriptors(entry.Key)
            dialogLines.Add(New DialogLine(MoodType.Info, $"+{entry.Value} {entryDescriptor.ItemTypeName}"))
        Next
        Return New OkDialog(
            "Get So Dismantled!",
            dialogLines,
            ItemsOfTypeDialog.LaunchMenu(character, descriptor.ItemType))
    End Function

    Private Function Drop() As IDialog
        character.RemoveItem(item)
        character.Location.AddItem(item)
        Return ItemsOfTypeDialog.LaunchMenu(character, item.ItemType).Invoke()
    End Function
End Class
