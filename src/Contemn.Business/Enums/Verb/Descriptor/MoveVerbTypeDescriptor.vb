Imports TGGD.Business

Friend Class MoveVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    ReadOnly directionType As String
    Public Sub New(verbType As String, directionType As String)
        MyBase.New(verbType, Nothing)
        Me.directionType = directionType
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim descriptor = directionType.ToDirectionTypeDescriptor
        Dim nextColumn = descriptor.GetNextColumn(character.Column)
        Dim nextRow = descriptor.GetNextRow(character.Row)
        Dim nextLocation = character.Map.GetLocation(nextColumn, nextRow)
        Return character.MoveTo(nextLocation)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function
End Class
