Friend MustInherit Class GeneratorTypeDescriptor
    Friend ReadOnly Property GeneratorType As String
    Sub New(generatorType As String)
        Me.GeneratorType = generatorType
    End Sub
    Friend MustOverride Sub Initialize(generator As IGenerator)
End Class
