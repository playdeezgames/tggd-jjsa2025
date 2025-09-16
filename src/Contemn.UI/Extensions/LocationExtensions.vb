Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module LocationExtensions
    Private ReadOnly locationPixelTable As IReadOnlyDictionary(Of String, Func(Of ILocation, Integer)) =
        New Dictionary(Of String, Func(Of ILocation, Integer)) From
        {
            {LocationType.Grass, AddressOf GrassToPixel},
            {LocationType.Tree, AddressOf TreeToPixel},
            {LocationType.Water, AddressOf WaterToPixel}
        }

    Private Function WaterToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(
            &HF7,
            Hue.Black,
            Hue.Blue)
    End Function

    Private Function TreeToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(
            &H9D,
            Hue.LightGreen,
            Hue.Black)
    End Function

    Private Function GrassToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("."),
            Hue.Green,
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
