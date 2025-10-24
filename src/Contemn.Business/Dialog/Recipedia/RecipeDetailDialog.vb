Imports TGGD.Business

Friend Class RecipeDetailDialog
    Inherits LegacyBaseDialog

    Private ReadOnly nextDialog As Func(Of IDialog)

    Public Sub New(descriptor As RecipeTypeDescriptor, nextDialog As Func(Of IDialog))
        MyBase.New(descriptor.Name, {New DialogChoice(OK_CHOICE, OK_TEXT)}, GenerateLines(descriptor))
        Me.nextDialog = nextDialog
    End Sub

    Private Shared Function GenerateLines(descriptor As RecipeTypeDescriptor) As IEnumerable(Of IDialogLine)
        Return descriptor.Describe()
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Return CancelDialog()
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return nextDialog.Invoke
    End Function
End Class
