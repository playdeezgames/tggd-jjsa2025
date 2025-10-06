Imports System.Runtime.CompilerServices

Friend Module ItemTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, ItemTypeDescriptor) =
        New List(Of ItemTypeDescriptor) From
        {
            New PlantFiberItemTypeDescriptor(),
            New TwineItemTypeDescriptor(),
            New FishingNetItemTypeDescriptor(),
            New FishItemTypeDescriptor(),
            New RockItemTypeDescriptor(),
            New SharpRockItemTypeDescriptor(),
            New StickItemTypeDescriptor(),
            New AxeItemTypeDescriptor(),
            New LogItemTypeDescriptor(),
            New BladeItemTypeDescriptor(),
            New KnifeItemTypeDescriptor(),
            New RawFishFiletItemTypeDescriptor(),
            New HammerItemTypeDescriptor(),
            New CarrotItemTypeDescriptor(),
            New SharpStickItemTypeDescriptor(),
            New ClayItemTypeDescriptor(),
            New UnfiredPotItemTypeDescriptor(),
            New CampFireItemTypeDescriptor(),
            New KilnItemTypeDescriptor(),
            New CookedFishFiletItemTypeDescriptor(),
            New CharcoalItemTypeDescriptor(),
            New FiredPotItemTypeDescriptor(),
            New FireStarterItemTypeDescriptor()
        }.ToDictionary(Function(x) x.ItemType, Function(x) x)
End Module
