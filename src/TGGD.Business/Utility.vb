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
    'Valid use of extension, because there is no repeat loop in VB
    <Extension>
    Public Sub Repeat(repeatCount As Integer, predicate As Action)
        For Each dummy In Enumerable.Range(0, repeatCount)
            predicate()
        Next
    End Sub
    'Valid <Extension> use, because dealing with generic collection
    <Extension>
    Public Function AppendIf(Of TSource)(source As IEnumerable(Of TSource), condition As Boolean, element As TSource) As IEnumerable(Of TSource)
        Return If(condition, source.Append(element), source)
    End Function
    'Valid <Extension> use, because dealing with generic collection
    <Extension>
    Public Function ConcatIf(Of TSource)(source As IEnumerable(Of TSource), condition As Boolean, elements As IEnumerable(Of TSource)) As IEnumerable(Of TSource)
        Return If(condition, source.Concat(elements), source)
    End Function
End Module
