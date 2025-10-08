Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module ItemExtensions
    Private ReadOnly itemPixelTable As IReadOnlyDictionary(Of String, Func(Of IItem, Integer)) =
        New Dictionary(Of String, Func(Of IItem, Integer)) From
        {
            {NameOf(PlantFiberItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(TwineItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(FishingNetItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(FishItemTypeDescriptor), AddressOf FishToPixel},
            {NameOf(RockItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(SharpRockItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(StickItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(AxeItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(LogItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(BladeItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(KnifeItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(RawFishFiletItemTypeDescriptor), AddressOf RawFishFiletToPixel},
            {NameOf(HammerItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(CarrotItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(SharpStickItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(ClayItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(UnfiredPotItemTypeDescriptor), AddressOf UnfiredPotToPixel},
            {NameOf(CampFireItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(KilnItemTypeDescriptor), AddressOf EliminateMe},
            {NameOf(CookedFishFiletItemTypeDescriptor), AddressOf CookedFishFiletToPixel},
            {NameOf(CharcoalItemTypeDescriptor), AddressOf CharcoalToPixel},
            {NameOf(FiredPotItemTypeDescriptor), AddressOf FiredPotToPixel},
            {NameOf(FireStarterItemTypeDescriptor), AddressOf FireStarterToPixel},
            {NameOf(TorchItemTypeDescriptor), AddressOf TorchToPixel}
        }

    Private Function CookedFishFiletToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE0,
            Hue.Brown,
            Hue.Black)
    End Function

    Private Function RawFishFiletToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE0,
            Hue.LightRed,
            Hue.Black)
    End Function

    Private Function FishToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            &HE0,
            Hue.LightGray,
            Hue.Black)
    End Function

    Private Function CharcoalToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            &HF9,
            Hue.DarkGray,
            Hue.Black)
    End Function

    Private Function UnfiredPotToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("U"),
            Hue.DarkGray,
            Hue.Black)
    End Function

    Private Function FiredPotToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("U"),
            If(
                item.IsStatisticAtMinimum(StatisticType.Water),
                Hue.LightGray,
                If(
                    item.GetTag(TagType.Safe),
                    Hue.LightBlue,
                    Hue.Blue)),
            Hue.Black)
    End Function

    Private Function FireStarterToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(&HE7, Hue.Brown, Hue.Black)
    End Function

    Private Function TorchToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(
            Asc("!"),
            If(
                item.GetTag(TagType.IsLit),
                Hue.Red,
                If(
                    item.IsStatisticAtMinimum(StatisticType.Fuel),
                    Hue.DarkGray,
                    Hue.Brown)),
            Hue.Black)
    End Function

    Private Function EliminateMe(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(Asc("?"), Hue.DarkGray, Hue.Black)
    End Function

    <Extension>
    Friend Function ToPixel(item As IItem) As Integer
        Dim pixelFunction As Func(Of IItem, Integer) = Nothing
        If itemPixelTable.TryGetValue(item.ItemType, pixelFunction) Then
            Return pixelFunction(item)
        End If
        Return UIBufferExtensions.ToPixel(Asc("?"), Hue.DarkGray, Hue.Black)
    End Function
End Module
