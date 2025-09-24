Friend Class GrassForageTableTypeDescriptor
    Inherits ForageTableTypeDescriptor

    Public Sub New()
        MyBase.New(Business.ForageTableType.Grass)
    End Sub

    Friend Overrides Sub Initialize(generator As IGenerator)
        generator.SetWeight(String.Empty, 5)
        generator.SetWeight(NameOf(PlantFiberItemTypeDescriptor), 20)
    End Sub
End Class
