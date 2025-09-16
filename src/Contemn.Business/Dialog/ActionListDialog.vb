Imports TGGD.Business

Friend Class ActionListDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter
    Sub New(character As ICharacter)
        MyBase.New("Actions...", GenerateChoices(character), Array.Empty(Of String))
        Me.character = character
    End Sub
    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each verbType In character.AvailableVerbs
            Dim descriptor = verbType.ToVerbTypeDescriptor
            result.Add((verbType, descriptor.VerbTypeName))
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
        Return Choose(NEVER_MIND_CHOICE)
    End Function
End Class
