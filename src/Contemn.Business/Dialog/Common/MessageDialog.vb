Imports TGGD.Business

Friend Class MessageDialog
    Inherits BaseDialog

    Private ReadOnly choiceTable As IReadOnlyDictionary(Of String, Func(Of IDialog))

    Public Sub New(
                  caption As String,
                  lines As IEnumerable(Of IDialogLine),
                  choices As IEnumerable(Of (Choice As String, Text As String, NextDialog As Func(Of IDialog), Enabled As Boolean)),
                  onCancelDialog As Func(Of IDialog))
        MyBase.New(
            caption,
            choices.Where(Function(x) x.Enabled).Select(Function(x) New DialogChoice(x.Choice, x.Text)),
            lines,
            onCancelDialog)
        Me.choiceTable = choices.Where(Function(x) x.Enabled).ToDictionary(Function(x) x.Choice, Function(x) x.NextDialog)
    End Sub

    Public Overrides Function Choose(choice As String) As IDialog
        Return choiceTable(choice).Invoke()
    End Function
End Class
