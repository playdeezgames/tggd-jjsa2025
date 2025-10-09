Imports TGGD.Business

Friend Class HowToCraftItemTypeDialog
    Inherits BaseDialog

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

    Private Shared Function GenerateLines(
                                         character As ICharacter,
                                         itemType As String) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(
                                           character As ICharacter,
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
                                           character As ICharacter,
                                           itemType As String) As String
        Return $"How to craft {ItemTypes.Descriptors(itemType).ItemTypeName}?"
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return ShowRecipe(choice)
        End Select
    End Function

    Private Function ShowRecipe(recipeType As String) As IDialog
        Return New RecipeDetailDialog(RecipeTypes.Descriptors(recipeType), Function() New HowToCraftItemTypeDialog(character, itemType))
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return New HowToCraftListDialog(character)
    End Function
End Class
