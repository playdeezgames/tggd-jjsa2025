Imports TGGD.Business

Friend Class BumpDialog
    Inherits EntityDialog(Of ICharacter)

    Public Sub New(character As ICharacter)
        MyBase.New(
            character,
            AddressOf GenerateCaption,
            AddressOf GenerateChoices,
            AddressOf GenerateLines,
            Function() Nothing)
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Return character.GetBumpLocation().GenerateBumpLines(character)
    End Function

    Private Shared Function GenerateCaption(character As ICharacter) As String
        Return character.GetBumpLocation().Descriptor.LocationTypeName
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each verbType In VerbTypes.AllOfCategory(VerbCategoryType.Bump)
            Dim descriptor = VerbTypes.Descriptors(verbType)
            If descriptor.CanPerform(character) Then
                result.Add(New DialogChoice(verbType, descriptor.VerbTypeName))
            End If
        Next
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return VerbTypes.Descriptors(choice).Perform(entity)
        End Select
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() New BumpDialog(character)
    End Function
End Class
