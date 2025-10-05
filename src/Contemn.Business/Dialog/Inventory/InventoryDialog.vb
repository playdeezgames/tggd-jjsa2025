Imports TGGD.Business

Friend Class InventoryDialog
    Inherits BaseDialog

    Private character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New("Inventory", GenerateChoices(character), GenerateLines(character))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each itemStack In character.Items.GroupBy(Function(x) x.ItemType)
            result.Add(New DialogChoice(itemStack.Key, $"{itemStack.First.Descriptor.ItemTypeName}({itemStack.Count})"))
        Next
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CharacterActionsDialog.LaunchMenu(character).Invoke()
            Case Else
                Return MakeItemTypeDialog(choice)
        End Select
    End Function

    Private Function MakeItemTypeDialog(itemType As String) As IDialog
        Dim descriptor = ItemTypes.Descriptors(itemType)
        If descriptor.IsAggregate Then
            Return New ItemTypeDialog(character, itemType)
        Else
            Return New ItemsOfTypeDialog(character, itemType)
        End If
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Choose(NEVER_MIND_CHOICE)
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
                    character.HasItems,
                    CType(New InventoryDialog(character), IDialog),
                    CharacterActionsDialog.LaunchMenu(character).Invoke())
    End Function
End Class
