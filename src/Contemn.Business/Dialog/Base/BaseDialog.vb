Imports TGGD.Business

Public MustInherit Class BaseDialog
    Implements IDialog
    Sub New(
           caption As String,
           choices As IEnumerable(Of (Choice As String, Text As String)),
           lines As IEnumerable(Of (Mood As String, Text As String)))
        Me.Caption = caption
        Me.LegacyChoices = choices
        Me.Choices = choices.Select(Function(x) New DialogChoice(x.Choice, x.Text))
        Me.Lines = lines.Select(Function(x) New DialogLine(x.Mood, x.Text))
    End Sub
    Public ReadOnly Property Caption As String Implements IDialog.Caption
    Public ReadOnly Property LegacyChoices As IEnumerable(Of (Choice As String, Text As String)) Implements IDialog.LegacyChoices
    Public ReadOnly Property Choices As IEnumerable(Of IDialogChoice) Implements IDialog.Choices
    Public ReadOnly Property Lines As IEnumerable(Of IDialogLine) Implements IDialog.Lines
    Public MustOverride Function Choose(choice As String) As IDialog Implements IDialog.Choose
    Public MustOverride Function CancelDialog() As IDialog Implements IDialog.CancelDialog
End Class
