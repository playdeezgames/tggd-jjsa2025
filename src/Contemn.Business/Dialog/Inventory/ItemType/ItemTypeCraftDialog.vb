Imports System.Reflection.Metadata.Ecma335
Imports TGGD.Business

Friend Class ItemTypeCraftDialog
    Inherits BaseDialog
    Private Shared ReadOnly CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)
    Private Const CRAFT_ANOTHER_TEXT = "Craft Another"

    Private ReadOnly character As ICharacter
    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            GenerateCaption(character, itemType),
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of (Mood As String, Text As String))
        Return Array.Empty(Of (Mood As String, Text As String))
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of (Choice As String, Text As String))
        Dim choices As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each entry In RecipeTypes.Descriptors.Where(Function(x) x.Value.HasInput(itemType) AndAlso x.Value.CanCraft(character))
            choices.Add((entry.Key, entry.Value.Name))
        Next
        Return choices
    End Function

    Private Shared Function GenerateCaption(character As ICharacter, itemType As String) As String
        Return $"Craft with {itemType.ToItemTypeDescriptor.ItemTypeName}..."
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return CraftRecipe(choice)
        End Select
    End Function

    Private Function CraftRecipe(recipeType As String) As IDialog
        Dim descriptor = recipeType.ToRecipeTypeDescriptor
        Dim messageLines = descriptor.Craft(character)
        character.PlaySfx(Sfx.Craft)
        character.ChangeStatistic(StatisticType.Score, 1)
        Return New MessageDialog(
            messageLines,
            {
                (OK_CHOICE, OK_TEXT, ItemTypeDialog.LaunchMenu(character, itemType), True),
                (CRAFT_ANOTHER_CHOICE, CRAFT_ANOTHER_TEXT, CraftAnother(recipeType), descriptor.CanCraft(character))
            }, ItemTypeDialog.LaunchMenu(character, itemType))
    End Function

    Private Function CraftAnother(recipeType As String) As Func(Of IDialog)
        Return Function() CraftRecipe(recipeType)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return New ItemTypeDialog(character, itemType)
    End Function
End Class
