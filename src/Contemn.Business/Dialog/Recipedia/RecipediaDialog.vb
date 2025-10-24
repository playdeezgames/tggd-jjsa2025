Imports TGGD.Business

Friend Class RecipediaDialog
    Inherits LegacyBaseDialog
    Private Shared ReadOnly HOW_TO_MAKE_CHOICE As String = NameOf(HOW_TO_MAKE_CHOICE)
    Const HOW_TO_MAKE_TEXT = "How to craft...?"

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New("Recipedia", GenerateChoices(), Array.Empty(Of IDialogLine))
        Me.character = character
    End Sub

    Private Shared Function GenerateChoices() As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT),
                New DialogChoice(HOW_TO_MAKE_CHOICE, HOW_TO_MAKE_TEXT)
            }
        result.AddRange(
            RecipeTypes.Descriptors.
            Select(Function(x) New DialogChoice(x.RecipeType, x.Name)))
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case HOW_TO_MAKE_CHOICE
                Return New HowToCraftListDialog(character)
            Case Else
                Return New RecipeDetailDialog(RecipeTypes.Descriptors.Single(Function(x) x.RecipeType = choice), Function() New RecipediaDialog(character))
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return CharacterActionsDialog.LaunchMenu(character).Invoke()
    End Function
End Class
