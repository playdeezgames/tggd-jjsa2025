Friend Class GrassForageGeneratorTypeDescriptor
    Inherits GeneratorTypeDescriptor

    Public Sub New()
        MyBase.New(Business.GeneratorType.GrassForage)
    End Sub

    Friend Overrides Sub Initialize(generator As IGenerator)
        generator.SetWeight(String.Empty, 5)
        generator.SetWeight(NameOf(PlantFiberItemTypeDescriptor), 20)
        generator.SetWeight(NameOf(CarrotItemTypeDescriptor), 5)
    End Sub
End Class
