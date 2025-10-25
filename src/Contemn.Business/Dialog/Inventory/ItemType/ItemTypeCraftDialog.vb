Imports TGGD.Business

Friend Class ItemTypeCraftDialog
    Inherits EntityDialog(Of ICharacter)
    Private Shared ReadOnly CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)

    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            character,
            Function(x) GenerateCaption(itemType),
            Function(x) GenerateChoices(x, itemType),
            Function(x) Array.Empty(Of IDialogLine),
            ItemTypeDialog.LaunchMenu(character, itemType))
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of IDialogChoice)
        Dim choices As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each entry In RecipeTypes.Descriptors.Values.Where(Function(x) x.HasInput(itemType) AndAlso x.CanCraft(character))
            choices.Add(New DialogChoice(entry.RecipeId.ToString, entry.Name))
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
                Return entity.CraftRecipe(CInt(choice), ItemTypeDialog.LaunchMenu(entity, itemType), False)
        End Select
    End Function
End Class
