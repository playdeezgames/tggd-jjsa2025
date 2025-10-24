Imports TGGD.Business

Friend Class CharacterActionsDialog
    Inherits EntityDialog(Of ICharacter)

    Sub New(character As ICharacter, verbCategoryType As String, caption As String)
        MyBase.New(
            character,
            Function(x) caption,
            Function(x) GenerateChoices(x, verbCategoryType),
            Function(x) Array.Empty(Of IDialogLine),
            Function() Nothing)
    End Sub

    Private Shared Function GenerateChoices(character As ICharacter, verbCategoryType As String) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each verbType In character.AvailableVerbsOfCategory(verbCategoryType)
            Dim descriptor = VerbTypes.LegacyDescriptors(verbType)
            result.Add(New DialogChoice(verbType, descriptor.VerbTypeName))
        Next
        Return result
    End Function
    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return VerbTypes.LegacyDescriptors(choice).Perform(entity)
        End Select
    End Function
End Class
