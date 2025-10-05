Imports System.Runtime.CompilerServices

Friend Module MapTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, MapTypeDescriptor) =
        New List(Of MapTypeDescriptor) From
        {
            New OverworldMapTypeDescriptor()
        }.ToDictionary(Function(x) x.MapType, Function(x) x)
End Module
