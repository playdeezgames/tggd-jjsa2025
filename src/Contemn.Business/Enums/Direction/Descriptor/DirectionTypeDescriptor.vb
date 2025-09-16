Friend Class DirectionTypeDescriptor
    Friend ReadOnly Property DirectionType As String
    Private ReadOnly deltaX As Integer
    Private ReadOnly deltaY As Integer
    Sub New(directionType As String, deltaX As Integer, deltaY As Integer)
        Me.DirectionType = directionType
        Me.deltaX = deltaX
        Me.deltaY = deltaY
    End Sub
    Function GetNextColumn(column As Integer) As Integer
        Return column + deltaX
    End Function
    Function GetNextRow(row As Integer) As Integer
        Return row + deltaY
    End Function
End Class
