Imports TGGD.Business

Friend MustInherit Class BaseDialog
    Implements IDialog

    Private ReadOnly onCancelDialog As Func(Of IDialog)
    Public ReadOnly Property Caption As String Implements IDialog.Caption
    Public ReadOnly Property Choices As IEnumerable(Of IDialogChoice) Implements IDialog.Choices
    Public ReadOnly Property Lines As IEnumerable(Of IDialogLine) Implements IDialog.Lines
    Public MustOverride Function Choose(choice As String) As IDialog Implements IDialog.Choose

    Public Sub New(
                  caption As String,
                  choices As IEnumerable(Of IDialogChoice),
                  lines As IEnumerable(Of IDialogLine),
                  onCancelDialog As Func(Of IDialog))
        Me.Caption = caption
        Me.Choices = choices
        Me.Lines = lines
        Me.onCancelDialog = onCancelDialog
    End Sub

    Public Function CancelDialog() As IDialog Implements IDialog.CancelDialog
        Return onCancelDialog()
    End Function
End Class
