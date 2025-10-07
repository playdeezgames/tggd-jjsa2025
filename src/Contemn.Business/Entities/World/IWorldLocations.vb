Public Interface IWorldLocations
    Function CreateLocation(locationType As String, map As IMap, column As Integer, row As Integer) As ILocation
    Function GetLocation(locationId As Integer) As ILocation
    Sub ActivateLocation(location As ILocation)
    Sub DeactivateLocation(location As ILocation)
    ReadOnly Property ActiveLocations As IEnumerable(Of ILocation)

End Interface
