Friend Class RockForageGeneratorTypeDescriptor
    Inherits GeneratorTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(RockForageGeneratorTypeDescriptor))
    End Sub

    Friend Overrides Sub Initialize(generator As IGenerator)
        generator.SetWeight(String.Empty, 5)
        generator.SetWeight(NameOf(RockItemTypeDescriptor), 20)
    End Sub
End Class
