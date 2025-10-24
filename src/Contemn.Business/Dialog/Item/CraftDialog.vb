Imports TGGD.Business

Friend Class CraftDialog
    Inherits CharacterDialog

    Public Sub New(character As ICharacter)
        MyBase.New(
            character,
            Function(x) "Craft...",
            AddressOf GenerateChoices,
            Function(x) Array.Empty(Of IDialogLine),
            CharacterActionsDialog.LaunchMenu(character))
    End Sub

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim choices As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each entry In RecipeTypes.Descriptors.Where(Function(x) x.CanCraft(character))
            choices.Add(New DialogChoice(entry.RecipeType, entry.Name))
        Next
        Return choices
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return character.CraftRecipe(
                    choice,
                    LaunchMenu(character),
                    False)
        End Select
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
            character.HasAvailableRecipes,
            New CraftDialog(character),
            CharacterActionsDialog.LaunchMenu(character).Invoke)
    End Function
End Class
