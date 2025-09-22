Imports TGGD.Business

Friend Class CraftDialog
    Inherits BaseDialog
    Private Shared ReadOnly CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)
    Private Const CRAFT_ANOTHER_TEXT = "Craft Another"

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(
            GenerateCaption(character),
            GenerateChoices(character),
            GenerateLines(character))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

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

    Private Shared Function GenerateCaption(character As ICharacter) As String
        Return $"Craft..."
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
        If RecipeTypes.Descriptors.Values.Any(Function(x) x.CanCraft(character)) Then
            Return Function() New CraftDialog(character)
        End If
        Return VerbListDialog.LaunchMenu(character)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return VerbListDialog.LaunchMenu(character).Invoke()
    End Function
End Class
