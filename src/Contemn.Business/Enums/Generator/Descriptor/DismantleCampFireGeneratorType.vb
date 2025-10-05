Friend Class DismantleCampFireGeneratorType
    Inherits GeneratorTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(DismantleCampFireGeneratorType))
    End Sub

    Friend Overrides Sub Initialize(generator As IGenerator)
        generator.SetWeight(Rock, 4)
    End Sub
End Class
