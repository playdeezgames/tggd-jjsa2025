Imports TGGD.Business

Friend Class GroundItemOfTypeDialog
    Inherits BaseDialog
    Shared ReadOnly TAKE_CHOICE As String = NameOf(TAKE_CHOICE)
    Const TAKE_TEXT = "Take"

    Private ReadOnly character As ICharacter
    Private ReadOnly item As IItem

    Public Sub New(
                  character As ICharacter,
                  item As IItem)
        MyBase.New(
            GenerateCaption(item),
            GenerateChoices(character, item),
            GenerateLines(character, item))
        Me.character = character
        Me.item = item
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, item As IItem) As IEnumerable(Of IDialogLine)
        Return item.Describe()
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, item As IItem) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT),
                New DialogChoice(TAKE_CHOICE, TAKE_TEXT)
            }
        Return result
    End Function

    Private Shared Function GenerateCaption(item As IItem) As String
        Return item.Name
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case TAKE_CHOICE
                Return Take()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Take() As IDialog
        character.Location.RemoveItem(item)
        character.AddItem(item)
        Return GroundItemsOfTypeDialog.LaunchMenu(character, item.ItemType).Invoke()
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return GroundItemsOfTypeDialog.LaunchMenu(character, item.ItemType).Invoke()
    End Function
End Class
