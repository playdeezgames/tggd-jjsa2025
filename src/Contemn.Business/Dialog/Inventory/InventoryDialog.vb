Imports TGGD.Business

Friend Class InventoryDialog
    Inherits BaseDialog

    Private character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New("Inventory", GenerateChoices(character), GenerateLines(character))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        Return Array.Empty(Of (Mood As String, Text As String))
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each itemStack In character.Items.GroupBy(Function(x) x.Name)
            Dim item = itemStack.First
            result.Add((item.ItemType, $"{item.Name}({itemStack.Count})"))
        Next
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return VerbType.ActionList.ToVerbTypeDescriptor.Perform(character)
            Case Else
                Return MakeItemTypeDialog(choice)
        End Select
    End Function

    Private Function MakeItemTypeDialog(itemType As String) As IDialog
        Dim descriptor = itemType.ToItemTypeDescriptor
        If descriptor.IsAggregate Then
            Return New ItemTypeDialog(character, itemType)
        Else
            Return New ItemsOfTypeDialog(character, itemType)
        End If
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return VerbType.ActionList.ToVerbTypeDescriptor.Perform(character)
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
                    character.HasItems,
                    CType(New InventoryDialog(character), IDialog),
                    VerbListDialog.LaunchMenu(character).Invoke())
    End Function
End Class
