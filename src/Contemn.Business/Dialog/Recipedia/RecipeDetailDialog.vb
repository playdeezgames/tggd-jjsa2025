Imports TGGD.Business

Friend Class RecipeDetailDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter, descriptor As RecipeTypeDescriptor)
        MyBase.New(descriptor.Name, {New DialogChoice(OK_CHOICE, OK_TEXT)}, GenerateLines(character, descriptor))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, descriptor As RecipeTypeDescriptor) As IEnumerable(Of IDialogLine)
        Return descriptor.Describe()
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Return CancelDialog()
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return New RecipediaDialog(character)
    End Function
End Class
