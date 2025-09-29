Imports TGGD.Business

Friend Class RecipediaDialog
    Inherits BaseDialog

    Private character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New("Recipedia", GenerateChoices(), Array.Empty(Of IDialogLine))
        Me.character = character
    End Sub

    Private Shared Function GenerateChoices() As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        result.AddRange(
            RecipeTypes.Descriptors.
            Select(Function(x) New DialogChoice(x.Key, x.Value.Name)))
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return New RecipeDetailDialog(character, choice.ToRecipeTypeDescriptor)
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return VerbListDialog.LaunchMenu(character).Invoke()
    End Function
End Class
