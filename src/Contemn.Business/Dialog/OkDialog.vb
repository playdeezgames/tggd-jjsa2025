Imports TGGD.Business

Friend Class OkDialog
    Inherits MessageDialog

    Shared ReadOnly OK_CHOICE As String = NameOf(OK_CHOICE)
    Const OK_TEXT = "OK"

    Public Sub New(lines As IEnumerable(Of String), nextDialog As Func(Of IDialog))
        MyBase.New(lines, {(OK_CHOICE, OK_TEXT, nextDialog)}, nextDialog)
    End Sub
End Class
