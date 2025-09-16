Imports TGGD.Business

Friend MustInherit Class BaseDialog
    Implements IDialog
    Sub New(
           caption As String,
           choices As IEnumerable(Of (Choice As String, Text As String)),
           lines As IEnumerable(Of String))
        Me.Caption = caption
        Me.Choices = choices
        Me.Lines = lines
    End Sub
    Public ReadOnly Property Caption As String Implements IDialog.Caption
    Public ReadOnly Property Choices As IEnumerable(Of (Choice As String, Text As String)) Implements IDialog.Choices
    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
    Public MustOverride Function Choose(choice As String) As IDialog Implements IDialog.Choose
    Public MustOverride Function CancelDialog() As IDialog Implements IDialog.CancelDialog
End Class
