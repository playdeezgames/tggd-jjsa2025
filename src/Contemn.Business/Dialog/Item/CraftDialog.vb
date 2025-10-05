Imports TGGD.Business

Friend Class CraftDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(
            "Craft...",
            GenerateChoices(character),
            Array.Empty(Of IDialogLine))
        Me.character = character
    End Sub

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim choices As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each entry In RecipeTypes.Descriptors.Where(Function(x) x.Value.CanCraft(character))
            choices.Add(New DialogChoice(entry.Key, entry.Value.Name))
        Next
        Return choices
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return character.CraftRecipe(choice, LaunchMenu(character))
        End Select
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
            RecipeTypes.Descriptors.Values.Any(Function(x) x.CanCraft(character)),
            New CraftDialog(character),
            CharacterActionsDialog.LaunchMenu(character).Invoke)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return CharacterActionsDialog.LaunchMenu(character).Invoke()
    End Function
End Class
