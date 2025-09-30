Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module LocationExtensions
    Private ReadOnly locationPixelTable As IReadOnlyDictionary(Of String, Func(Of ILocation, Integer)) =
        New Dictionary(Of String, Func(Of ILocation, Integer)) From
        {
            {LocationType.Dirt, AddressOf DirtToPixel}
        }
    Private Function DirtToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("."),
            Hue.Brown,
            Hue.Black)
    End Function

    <Extension>
    Friend Function ToPixel(location As ILocation) As Integer
        Return locationPixelTable(location.LocationType)(location)
    End Function
End Module
