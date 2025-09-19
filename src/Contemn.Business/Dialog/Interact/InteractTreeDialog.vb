
Imports TGGD.Business

Friend Class InteractTreeDialog
    Inherits BaseDialog

    Private character As ICharacter
    Private location As ILocation

    Public Sub New(character As ICharacter, location As ILocation)
        MyBase.New("Tree", GenerateChoices(), GenerateLines())
        Me.character = character
        Me.location = location
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of (Mood As String, Text As String))
        Return Array.Empty(Of (Mood As String, Text As String))
    End Function

    Private Shared Function GenerateChoices() As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return Nothing
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
