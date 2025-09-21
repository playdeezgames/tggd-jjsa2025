Imports TGGD.Business

Friend Class ItemOfTypeDialog
    Inherits BaseDialog

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

    Private Shared Function GenerateLines(character As ICharacter, item As IItem) As IEnumerable(Of (Mood As String, Text As String))
        Return item.Describe()
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, item As IItem) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)}
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
            Case Else
                Return item.MakeChoice(character, choice)
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return ItemsOfTypeDialog.LaunchMenu(Character, Item.ItemType).Invoke()
    End Function
End Class
