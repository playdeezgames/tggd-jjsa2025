Imports TGGD.Business

Friend Class DigClayVerbTypeDescriptor
    Inherits ToolBumpVerbTypeDescriptor
    Public Sub New()
        MyBase.New(
            Business.VerbType.DigClay,
            "Dig Clay",
            NameOf(ClayItemTypeDescriptor),
            TagType.CanDig,
            TagType.IsDiggable,
            {NameOf(WaterLocationTypeDescriptor)},
            "You find nothing.")
    End Sub
End Class
