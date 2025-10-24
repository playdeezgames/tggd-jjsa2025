Imports TGGD.Business

Friend Class ExamineLocationCharacterDialog
    Inherits BaseDialog

    Public Sub New(location As ILocation)
        MyBase.New(
            GenerateCaption(location),
            GenerateChoices(location),
            GenerateLines(location),
            Function() New ExamineLocationDialog(location))
    End Sub

    Private Shared Function GenerateLines(location As ILocation) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(location As ILocation) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Return result
    End Function

    Private Shared Function GenerateCaption(location As ILocation) As String
        Return location.Character.Name
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
