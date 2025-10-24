Imports TGGD.Business

Friend Class ConfirmDialog
    Inherits BaseDialog

    Private Shared ReadOnly NO_CHOICE As String = NameOf(NO_CHOICE)
    Const NO_TEXT = "No"

    Private Shared ReadOnly YES_CHOICE As String = NameOf(YES_CHOICE)
    Const YES_TEXT = "Yes"
    Private ReadOnly yesDialog As Func(Of IDialog)

    Public Sub New(
                  caption As String,
                  lines As IEnumerable(Of IDialogLine),
                  yesDialog As Func(Of IDialog),
                  noDialog As Func(Of IDialog))
        MyBase.New(
            caption,
            {
                New DialogChoice(NO_CHOICE, NO_TEXT),
                New DialogChoice(YES_CHOICE, YES_TEXT)
            },
            lines,
            noDialog)
        Me.yesDialog = yesDialog
    End Sub

    Public Overrides Function Choose(
                                    choice As String) As IDialog
        Select Case choice
            Case YES_CHOICE
                Return yesDialog()
            Case NO_CHOICE
                Return CancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
