Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module LocationExtensions
    Private ReadOnly locationPixelTable As IReadOnlyDictionary(Of String, Func(Of ILocation, Integer)) =
        New Dictionary(Of String, Func(Of ILocation, Integer)) From
        {
            {LocationType.Wall, AddressOf WallToPixel},
            {LocationType.Floor, AddressOf FloorToPixel}
        }

    Private Function FloorToPixel(location As ILocation) As Integer

        Return UIBufferExtensions.ToPixel(
            Asc("."),
            Hue.LightGray,
            Hue.Black)
    End Function

    Private Function WallToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("#"), Hue.Black, Hue.Blue)
    End Function

    <Extension>
    Friend Function ToPixel(location As ILocation) As Integer
        Return locationPixelTable(location.LocationType)(location)
    End Function
End Module
