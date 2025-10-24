Imports TGGD.Business

Friend Class RecipeDetailDialog
    Inherits BaseDialog

    Public Sub New(
                  descriptor As RecipeTypeDescriptor,
                  nextDialog As Func(Of IDialog))
        MyBase.New(
            descriptor.Name,
            {New DialogChoice(OK_CHOICE, OK_TEXT)},
            GenerateLines(descriptor),
            nextDialog)
    End Sub

    Private Shared Function GenerateLines(descriptor As RecipeTypeDescriptor) As IEnumerable(Of IDialogLine)
        Return descriptor.Describe()
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Return CancelDialog()
    End Function
End Class
