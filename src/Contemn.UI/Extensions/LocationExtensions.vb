Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module LocationExtensions
    Private ReadOnly locationPixelTable As IReadOnlyDictionary(Of String, Func(Of ILocation, Boolean, Integer)) =
        New Dictionary(Of String, Func(Of ILocation, Boolean, Integer)) From
        {
            {NameOf(GrassLocationTypeDescriptor), AddressOf GrassToPixel},
            {NameOf(TreeLocationTypeDescriptor), AddressOf TreeToPixel},
            {NameOf(WaterLocationTypeDescriptor), AddressOf WaterToPixel},
            {NameOf(DirtLocationTypeDescriptor), AddressOf DirtToPixel},
            {NameOf(RockLocationTypeDescriptor), AddressOf RockToPixel},
            {NameOf(CampFireLocationTypeDescriptor), AddressOf CampFireToPixel},
            {NameOf(KilnLocationTypeDescriptor), AddressOf KilnToPixel},
            {NameOf(TutorialHouseLocationTypeDescriptor), AddressOf TutorialHouseToPixel}
        }

    Private Function TutorialHouseToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            127,
            Hue.White,
            Hue.Black,
            invert)
    End Function

    Private Function KilnToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            8,
            Hue.DarkGray,
            If(location.GetTag(TagType.IsLit), Hue.Red, Hue.Black),
            invert)
    End Function

    Private Function CampFireToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            15,
            If(location.GetTag(TagType.IsLit), Hue.Red, Hue.Brown),
            Hue.Black,
            invert)
    End Function

    Private Function RockToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            7,
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function DirtToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("."),
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function WaterToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HF7,
            Hue.Black,
            Hue.Blue,
            invert)
    End Function

    Private Function TreeToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &H9D,
            Hue.LightGreen,
            Hue.Black,
            invert)
    End Function

    Private Function GrassToPixel(location As ILocation, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("."),
            Hue.Green,
            Hue.Black,
            invert)
    End Function

    <Extension>
    Friend Function ToPixel(location As ILocation, invert As Boolean) As Integer
        Return locationPixelTable(location.LocationType)(location, invert)
    End Function
End Module
