Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module ItemExtensions
    Private ReadOnly itemPixelTable As IReadOnlyDictionary(Of String, Func(Of IItem, Boolean, Integer)) =
        New Dictionary(Of String, Func(Of IItem, Boolean, Integer)) From
        {
            {NameOf(PlantFiberItemTypeDescriptor), AddressOf PlantFiberToPixel},
            {NameOf(TwineItemTypeDescriptor), AddressOf TwineToPixel},
            {NameOf(FishingNetItemTypeDescriptor), AddressOf FishingNetToPixel},
            {NameOf(FishItemTypeDescriptor), AddressOf FishToPixel},
            {NameOf(RockItemTypeDescriptor), AddressOf RockToPixel},
            {NameOf(SharpRockItemTypeDescriptor), AddressOf SharpRockToPixel},
            {NameOf(StickItemTypeDescriptor), AddressOf StickToPixel},
            {NameOf(AxeItemTypeDescriptor), AddressOf AxeToPixel},
            {NameOf(LogItemTypeDescriptor), AddressOf LogToPixel},
            {NameOf(BladeItemTypeDescriptor), AddressOf BladeToPixel},
            {NameOf(KnifeItemTypeDescriptor), AddressOf KnifeToPixel},
            {NameOf(RawFishFiletItemTypeDescriptor), AddressOf RawFishFiletToPixel},
            {NameOf(HammerItemTypeDescriptor), AddressOf HammerToPixel},
            {NameOf(CarrotItemTypeDescriptor), AddressOf CarrotToPixel},
            {NameOf(SharpStickItemTypeDescriptor), AddressOf SharpStickToPixel},
            {NameOf(ClayItemTypeDescriptor), AddressOf ClayToPixel},
            {NameOf(UnfiredPotItemTypeDescriptor), AddressOf UnfiredPotToPixel},
            {NameOf(CampFireItemTypeDescriptor), AddressOf CampFireToPixel},
            {NameOf(KilnItemTypeDescriptor), AddressOf KilnToPixel},
            {NameOf(CookedFishFiletItemTypeDescriptor), AddressOf CookedFishFiletToPixel},
            {NameOf(CharcoalItemTypeDescriptor), AddressOf CharcoalToPixel},
            {NameOf(FiredPotItemTypeDescriptor), AddressOf FiredPotToPixel},
            {NameOf(FireStarterItemTypeDescriptor), AddressOf FireStarterToPixel},
            {NameOf(TorchItemTypeDescriptor), AddressOf TorchToPixel},
            {NameOf(PlankItemTypeDescriptor), AddressOf PlankToPixel},
            {NameOf(FireBowItemTypeDescriptor), AddressOf FireBowToPixel},
            {NameOf(ShortStickItemTypeDescriptor), AddressOf ShortStickToPixel},
            {NameOf(BowDrillItemTypeDescriptor), AddressOf BowDrillToPixel}
        }

    Private Function BowDrillToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(&HE4, Hue.DarkGray, Hue.Black, invert)
    End Function

    Private Function ShortStickToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(Asc("-"), Hue.DarkGray, Hue.Black, invert)
    End Function

    Private Function FireBowToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(Asc("D"), Hue.Brown, Hue.Black, invert)
    End Function

    Private Function PlankToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(254, Hue.Brown, Hue.Black, invert)
    End Function

    Private Function CampFireToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            15,
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function KilnToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            8,
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function ClayToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HFE,
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function SharpStickToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("l"),
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function CarrotToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("^"),
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function HammerToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("T"),
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function KnifeToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("l"),
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function BladeToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("`"),
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function LogToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &H7,
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function AxeToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE2,
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function StickToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("/"),
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function SharpRockToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("'"),
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function RockToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("."),
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function FishingNetToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("#"),
            Hue.Green,
            Hue.Black,
            invert)
    End Function

    Private Function TwineToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HB3,
            Hue.Green,
            Hue.Black,
            invert)
    End Function

    Private Function PlantFiberToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("~"),
            Hue.Green,
            Hue.Black,
            invert)
    End Function

    Private Function CookedFishFiletToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE0,
            Hue.Brown,
            Hue.Black,
            invert)
    End Function

    Private Function RawFishFiletToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE0,
            Hue.LightRed,
            Hue.Black,
            invert)
    End Function

    Private Function FishToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE0,
            Hue.LightGray,
            Hue.Black,
            invert)
    End Function

    Private Function CharcoalToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            &HF9,
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function UnfiredPotToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("U"),
            Hue.DarkGray,
            Hue.Black,
            invert)
    End Function

    Private Function FiredPotToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("U"),
            If(
                item.IsStatisticAtMinimum(StatisticType.Water),
                Hue.LightGray,
                If(
                    item.GetTag(TagType.Safe),
                    Hue.LightBlue,
                    Hue.Blue)),
            Hue.Black,
            invert)
    End Function

    Private Function FireStarterToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(&HE7, Hue.Brown, Hue.Black, invert)
    End Function

    Private Function TorchToPixel(item As IItem, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("!"),
            If(
                item.GetTag(TagType.IsLit),
                Hue.Red,
                If(
                    item.IsStatisticAtMinimum(StatisticType.Fuel),
                    Hue.DarkGray,
                    Hue.Brown)),
            Hue.Black,
            invert)
    End Function
    'Valid use of <Extension>, because item should not know about pixels
    <Extension>
    Friend Function ToPixel(item As IItem, invert As Boolean) As Integer
        Dim pixelFunction As Func(Of IItem, Boolean, Integer) = Nothing
        If itemPixelTable.TryGetValue(item.ItemType, pixelFunction) Then
            Return pixelFunction(item, invert)
        End If
        Return UIBufferExtensions.ToPixel(Asc("?"), Hue.DarkGray, Hue.Black, invert)
    End Function
End Module
