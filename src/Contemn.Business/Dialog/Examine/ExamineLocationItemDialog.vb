Imports TGGD.Business

Friend Class ExamineLocationItemDialog
    Inherits LocationDialog

    Public Sub New(location As ILocation, item As IItem)
        MyBase.New(
            location,
            Function(x) GenerateCaption(item),
            Function(x) GenerateChoices(item),
            Function(x) GenerateLines(item),
            Function() New ExamineLocationItemsDialog(location))
    End Sub

    Private Shared Function GenerateLines(item As IItem) As IEnumerable(Of IDialogLine)
        Return item.Describe()
    End Function

    Private Shared Function GenerateChoices(item As IItem) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
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
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
