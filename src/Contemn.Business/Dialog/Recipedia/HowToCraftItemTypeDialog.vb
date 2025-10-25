Imports TGGD.Business

Friend Class HowToCraftItemTypeDialog
    Inherits EntityDialog(Of ICharacter)

    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            character,
            Function(x) GenerateCaption(itemType),
            Function(x) GenerateChoices(itemType),
            Function(x) GenerateLines(),
            Function() New HowToCraftListDialog(character))
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(
                                           itemType As String) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Dim recipes = RecipeTypes.Descriptors.Values.Where(Function(x) x.OutputItemTypes.Contains(itemType))
        result.AddRange(recipes.Select(Function(x) New DialogChoice(x.RecipeType, x.Name)))
        Return result
    End Function

    Private Shared Function GenerateCaption(
                                           itemType As String) As String
        Return $"How to craft {ItemTypes.Descriptors(itemType).ItemTypeName}?"
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return ShowRecipe(RecipeTypes.Descriptors.Values.Single(Function(x) x.RecipeType = choice))
        End Select
    End Function

    Private Function ShowRecipe(recipeDescriptor As RecipeTypeDescriptor) As IDialog
        Return New RecipeDetailDialog(recipeDescriptor, Function() New HowToCraftItemTypeDialog(entity, itemType))
    End Function
End Class
