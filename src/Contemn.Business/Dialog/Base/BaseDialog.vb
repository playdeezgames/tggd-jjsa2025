Imports TGGD.Business

Public MustInherit Class BaseDialog
    Inherits LegacyBaseDialog

    Private ReadOnly onCancelDialog As Func(Of IDialog)

    Public Sub New(
                  caption As String,
                  choices As IEnumerable(Of IDialogChoice),
                  lines As IEnumerable(Of IDialogLine),
                  onCancelDialog As Func(Of IDialog))
        MyBase.New(
            caption,
            choices,
            lines)
        Me.onCancelDialog = onCancelDialog
    End Sub

    Public Overrides Function CancelDialog() As IDialog
        Return onCancelDialog()
    End Function
End Class
