Imports System.Runtime.CompilerServices

Friend Module VerbTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, VerbTypeDescriptor) =
        New List(Of VerbTypeDescriptor) From
        {
            New MoveVerbTypeDescriptor(VerbType.MoveNorth, DirectionType.North),
            New MoveVerbTypeDescriptor(VerbType.MoveEast, DirectionType.East),
            New MoveVerbTypeDescriptor(VerbType.MoveSouth, DirectionType.South),
            New MoveVerbTypeDescriptor(VerbType.MoveWest, DirectionType.West),
            New ActionListVerbTypeDescriptor(),
            New ForageVerbTypeDescriptor(),
            New InventoryVerbTypeDescriptor(),
            New DrinkVerbTypeDescriptor()
        }.ToDictionary(Function(x) x.VerbType, Function(x) x)
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    Friend Function AllOfCategory(verbCategoryType As String) As IEnumerable(Of String)
        Return Descriptors.Values.Where(Function(x) x.VerbCategoryType = verbCategoryType).Select(Function(x) x.VerbType)
    End Function
    <Extension>
    Function ToVerbTypeDescriptor(verbType As String) As VerbTypeDescriptor
        Return Descriptors(verbType)
    End Function
End Module
