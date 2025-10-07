Public Interface IWorldMaps
    Function CreateMap(mapType As String) As IMap
    ReadOnly Property Maps As IEnumerable(Of IMap)
    Function GetMap(mapId As Integer) As IMap
End Interface
