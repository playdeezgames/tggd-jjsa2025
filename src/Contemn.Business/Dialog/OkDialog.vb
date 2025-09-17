Imports TGGD.Business

Friend Class OkDialog
    Inherits MessageDialog


    Public Sub New(lines As IEnumerable(Of String), nextDialog As Func(Of IDialog))
        MyBase.New(lines, {(OK_CHOICE, OK_TEXT, nextDialog)}, nextDialog)
    End Sub
End Class
