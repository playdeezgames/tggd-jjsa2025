Imports System.Runtime.CompilerServices

Friend Module VerbTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, VerbTypeDescriptor) =
        New List(Of VerbTypeDescriptor) From
        {
            New MoveVerbTypeDescriptor(VerbType.MoveNorth, DirectionType.North),
            New MoveVerbTypeDescriptor(VerbType.MoveEast, DirectionType.East),
            New MoveVerbTypeDescriptor(VerbType.MoveSouth, DirectionType.South),
            New MoveVerbTypeDescriptor(VerbType.MoveWest, DirectionType.West),
            New ActionListVerbTypeDescriptor()
        }.ToDictionary(Function(x) x.VerbType, Function(x) x)
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    <Extension>
    Function ToVerbTypeDescriptor(verbType As String) As VerbTypeDescriptor
        Return Descriptors(verbType)
    End Function
End Module
