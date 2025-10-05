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
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    <Extension>
    Function ToGeneratorTypeDescriptor(statisticType As String) As GeneratorTypeDescriptor
        Return Descriptors(statisticType)
    End Function
End Module
