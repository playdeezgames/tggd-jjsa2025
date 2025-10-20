Imports System.Runtime.CompilerServices

Friend Module ItemTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, ItemTypeDescriptor) =
        New List(Of ItemTypeDescriptor) From
        {
            New AxeItemTypeDescriptor(),
            New BladeItemTypeDescriptor(),
            New BowDrillItemTypeDescriptor(),
            New CampFireItemTypeDescriptor(),
            New CarrotItemTypeDescriptor(),
            New CharcoalItemTypeDescriptor(),
            New ClayItemTypeDescriptor(),
            New CookedFishFiletItemTypeDescriptor(),
            New FireBowItemTypeDescriptor(),
            New FiredPotItemTypeDescriptor(),
            New FireStarterItemTypeDescriptor(),
            New FishItemTypeDescriptor(),
            New FishingNetItemTypeDescriptor(),
            New HammerItemTypeDescriptor(),
            New KilnItemTypeDescriptor(),
            New KnifeItemTypeDescriptor(),
            New LogItemTypeDescriptor(),
            New PlankItemTypeDescriptor(),
            New PlantFiberItemTypeDescriptor(),
            New RawFishFiletItemTypeDescriptor(),
            New RockItemTypeDescriptor(),
            New SharpRockItemTypeDescriptor(),
            New SharpStickItemTypeDescriptor(),
            New ShortStickItemTypeDescriptor(),
            New StickItemTypeDescriptor(),
            New TorchItemTypeDescriptor(),
            New TwineItemTypeDescriptor(),
            New UnfiredPotItemTypeDescriptor()
        }.ToDictionary(Function(x) x.ItemType, Function(x) x)
End Module
