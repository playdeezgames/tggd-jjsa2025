Imports TGGD.Business

Friend Class ItemsOfTypeDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter
    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            itemType.ToItemTypeDescriptor.ItemTypeName,
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of String)
        Return Array.Empty(Of String)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return VerbType.Inventory.ToVerbTypeDescriptor.Perform(character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return VerbType.Inventory.ToVerbTypeDescriptor.Perform(character)
    End Function
End Class
