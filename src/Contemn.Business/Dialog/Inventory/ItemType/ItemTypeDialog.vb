Imports TGGD.Business

Friend Class ItemTypeDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter
    Private ReadOnly itemType As String
    Private Shared ReadOnly CRAFT_CHOICE As String = NameOf(CRAFT_CHOICE)
    Private Const CRAFT_TEXT = "Craft..."

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            itemType.ToItemTypeDescriptor.ItemTypeName,
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of (Mood As String, Text As String))
        Return {(MoodType.Info, $"You have {character.GetCountOfItemType(itemType)}.")}
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        If RecipeTypes.Descriptors.Any(Function(x) x.Value.HasInput(itemType) AndAlso x.Value.CanCraft(character)) Then
            result.Add((CRAFT_CHOICE, CRAFT_TEXT))
        End If
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return VerbType.Inventory.ToVerbTypeDescriptor.Perform(character)
            Case CRAFT_CHOICE
                Return New ItemTypeCraftDialog(character, itemType)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return VerbType.Inventory.ToVerbTypeDescriptor.Perform(character)
    End Function
End Class
