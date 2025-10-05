Imports System.Runtime.CompilerServices

Friend Module ForageTableTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, ForageTableTypeDescriptor) =
        New List(Of ForageTableTypeDescriptor) From
        {
            New GrassForageTableTypeDescriptor(),
            New RockForageTableTypeDescriptor()
        }.ToDictionary(Function(x) x.ForageTableType, Function(x) x)
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    <Extension>
    Function ToForageTableTypeDescriptor(statisticType As String) As ForageTableTypeDescriptor
        Return Descriptors(statisticType)
    End Function
End Module
