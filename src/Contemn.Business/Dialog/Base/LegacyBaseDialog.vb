Imports TGGD.Business

Friend MustInherit Class LegacyBaseDialog
    Implements IDialog
    Sub New(
           caption As String,
           choices As IEnumerable(Of IDialogChoice),
           lines As IEnumerable(Of IDialogLine))
        Me.Caption = caption
        Me.Choices = choices
        Me.Lines = lines
    End Sub
    Public ReadOnly Property Caption As String Implements IDialog.Caption
    Public ReadOnly Property Choices As IEnumerable(Of IDialogChoice) Implements IDialog.Choices
    Public ReadOnly Property Lines As IEnumerable(Of IDialogLine) Implements IDialog.Lines
    Public MustOverride Function Choose(choice As String) As IDialog Implements IDialog.Choose
    Public MustOverride Function CancelDialog() As IDialog Implements IDialog.CancelDialog
End Class
