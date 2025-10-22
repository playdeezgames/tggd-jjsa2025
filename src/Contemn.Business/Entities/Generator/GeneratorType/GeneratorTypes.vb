Imports System.Runtime.CompilerServices

Friend Module GeneratorTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, GeneratorTypeDescriptor) =
        New List(Of GeneratorTypeDescriptor) From
        {
            New GrassForageGeneratorTypeDescriptor(),
            New RockForageGeneratorTypeDescriptor(),
            New DismantleCampFireGeneratorType(),
            New DismantleKilnGeneratorType()
        }.ToDictionary(Function(x) x.GeneratorType, Function(x) x)
End Module
