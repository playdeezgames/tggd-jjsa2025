Imports System.Runtime.CompilerServices

Public Module Utility
    Public Function MakeHashSet(Of T)(ParamArray items() As T) As HashSet(Of T)
        Return New HashSet(Of T)(items)
    End Function
    Public Function MakeDictionary(Of T, U)(ParamArray entries() As (T, U)) As IReadOnlyDictionary(Of T, U)
        Return entries.ToDictionary(Of T, U)(
            Function(x) x.Item1, Function(x) x.Item2)
    End Function
    Public Function MakeList(Of T)(ParamArray items() As T) As IReadOnlyList(Of T)
        Return items.ToList
    End Function
    <Extension>
    Public Function AppendIf(Of TSource)(source As IEnumerable(Of TSource), element As TSource, condition As Boolean) As IEnumerable(Of TSource)
        Return If(condition, source.Append(element), source)
    End Function
End Module
