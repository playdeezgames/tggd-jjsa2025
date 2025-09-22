Imports TGGD.Business

Friend Class BumpDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(GenerateCaption(character), GenerateChoices(character), GenerateLines())
        Me.character = character
    End Sub

    Private Shared Function GenerateCaption(character As ICharacter) As String
        Return character.GetBumpLocation().LocationType.ToLocationTypeDescriptor.LocationTypeName
    End Function

    Private Shared Function GenerateLines() As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each verbType In VerbTypes.AllOfCategory(VerbCategoryType.Bump)
            Dim descriptor = verbType.ToVerbTypeDescriptor
            If descriptor.CanPerform(character) Then
                result.Add((verbType, descriptor.VerbTypeName))
            End If
        Next
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return Nothing
            Case Else
                Return choice.ToVerbTypeDescriptor.Perform(character)
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
