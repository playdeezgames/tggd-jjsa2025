Imports TGGD.Business

Friend Class ItemOfTypeDialog
    Inherits BaseDialog
    Shared ReadOnly DROP_CHOICE As String = NameOf(DROP_CHOICE)
    Const DROP_TEXT = "Drop"

    Private ReadOnly character As ICharacter
    Private ReadOnly item As IItem

    Public Sub New(
                  character As ICharacter,
                  item As IItem)
        MyBase.New(
            GenerateCaption(item),
            GenerateChoices(character, item),
            GenerateLines(item))
        Me.character = character
        Me.item = item
    End Sub

    Private Shared Function GenerateLines(item As IItem) As IEnumerable(Of IDialogLine)
        Return item.Describe()
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, item As IItem) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT),
                New DialogChoice(DROP_CHOICE, DROP_TEXT)
            }
        result.AddRange(item.GetAvailableChoices(character))
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
            Case Else
                Return item.MakeChoice(character, choice)
        End Select
    End Function

    Private Function Drop() As IDialog
        character.RemoveItem(item)
        character.Location.AddItem(item)
        Return ItemsOfTypeDialog.LaunchMenu(character, item.ItemType).Invoke()
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return ItemsOfTypeDialog.LaunchMenu(Character, Item.ItemType).Invoke()
    End Function
End Class
