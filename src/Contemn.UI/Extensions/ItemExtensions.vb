Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module ItemExtensions
    Private ReadOnly itemPixelTable As IReadOnlyDictionary(Of String, Func(Of IItem, Integer)) =
        New Dictionary(Of String, Func(Of IItem, Integer)) From
        {
            {NameOf(PlantFiberItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(TwineItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(FishingNetItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(FishItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(RockItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(SharpRockItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(StickItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(AxeItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(LogItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(BladeItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(KnifeItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(RawFishFiletItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(HammerItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(CarrotItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(SharpStickItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(ClayItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(UnfiredPotItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(CampFireItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(KilnItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(CookedFishFiletItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(CharcoalItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(FiredPotItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(FireStarterItemTypeDescriptor), AddressOf N00bToPixel},
            {NameOf(TorchItemTypeDescriptor), AddressOf N00bToPixel}
        }

    Private Function N00bToPixel(item As IItem) As Integer
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
