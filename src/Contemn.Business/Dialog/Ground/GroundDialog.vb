Imports TGGD.Business

Friend Class GroundDialog
    Inherits LegacyBaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(
            "Ground",
            GenerateChoices(character),
            Array.Empty(Of IDialogLine))
        Me.character = character
    End Sub

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each itemStack In character.Location.Items.GroupBy(Function(x) x.ItemType)
            result.Add(New DialogChoice(itemStack.Key, $"{itemStack.First.Descriptor.ItemTypeName}({itemStack.Count})"))
        Next
        Return result
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
                    character.Location.HasItems,
                    CType(New GroundDialog(character), IDialog),
                    CharacterActionsDialog.LaunchMenu(character).Invoke())
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return MakeItemTypeDialog(choice)
        End Select
    End Function

    Private Function MakeItemTypeDialog(itemType As String) As IDialog
        Dim descriptor = ItemTypes.Descriptors(itemType)
        If descriptor.IsAggregate Then
            Return New GroundItemTypeDialog(character, itemType)
        Else
            Return New GroundItemsOfTypeDialog(character, itemType)
        End If
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return CharacterActionsDialog.LaunchMenu(character).Invoke()
    End Function
End Class
