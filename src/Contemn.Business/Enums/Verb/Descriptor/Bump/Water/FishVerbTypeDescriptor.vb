Friend Class FishVerbTypeDescriptor
    Inherits ToolBumpVerbTypeDescriptor
    Public Sub New()
        MyBase.New(
            NameOf(FishVerbTypeDescriptor),
            "Fish",
            NameOf(FishItemTypeDescriptor),
            TagType.CanFish,
            TagType.IsFishable,
            {NameOf(WaterLocationTypeDescriptor)},
            "You catch nothing.")
    End Sub
End Class
