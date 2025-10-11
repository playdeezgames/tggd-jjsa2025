Imports System.Reflection.Metadata.Ecma335
Imports TGGD.Business

Friend Class ItemTypeCraftDialog
    Inherits BaseDialog
    Private Shared ReadOnly CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)

    Private ReadOnly character As ICharacter
    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            GenerateCaption(itemType),
            GenerateChoices(character, itemType),
            Array.Empty(Of IDialogLine))
        Me.character = character
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of IDialogChoice)
        Dim choices As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each entry In RecipeTypes.Descriptors.Where(Function(x) x.HasInput(itemType) AndAlso x.CanCraft(character))
            choices.Add(New DialogChoice(entry.RecipeType, entry.Name))
        Next
        Return choices
    End Function

    Private Shared Function GenerateCaption(itemType As String) As String
        Return $"Craft with {ItemTypes.Descriptors(itemType).ItemTypeName}..."
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return character.CraftRecipe(RecipeTypes.Descriptors.Single(Function(x) x.RecipeType = choice), ItemTypeDialog.LaunchMenu(character, itemType), False)
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return New ItemTypeDialog(character, itemType)
    End Function
End Class
