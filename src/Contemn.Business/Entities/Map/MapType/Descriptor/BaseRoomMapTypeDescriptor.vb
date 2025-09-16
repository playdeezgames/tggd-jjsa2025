Friend MustInherit Class BaseRoomMapTypeDescriptor
    Inherits MapTypeDescriptor

    Public Sub New(mapType As String, mapCount As Integer)
        MyBase.New(mapType, mapCount)
    End Sub

    Friend Overrides Sub OnInitialize(map As IMap)
        map.Columns = ROOM_COLUMNS
        map.Rows = ROOM_ROWS
        For Each column In Enumerable.Range(0, map.Columns)
            For Each row In Enumerable.Range(0, map.Rows)
                Dim locationType = Business.LocationType.Grass
                map.World.CreateLocation(locationType, map, column, row)
            Next
        Next
    End Sub
End Class
