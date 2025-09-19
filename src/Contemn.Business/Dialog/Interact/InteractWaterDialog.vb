Imports TGGD.Business

Friend Class InteractWaterDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New("Water", GenerateChoices(character), GenerateLines())
        Me.character = character
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of (Mood As String, Text As String))
        Return Array.Empty(Of (Mood As String, Text As String))
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
