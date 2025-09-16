Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module ItemExtensions
    Private ReadOnly itemPixelTable As IReadOnlyDictionary(Of String, Func(Of IItem, Integer)) =
        New Dictionary(Of String, Func(Of IItem, Integer)) From
        {
        }

    <Extension>
    Friend Function ToPixel(item As IItem) As Integer
        Return itemPixelTable(item.ItemType)(item)
    End Function

End Module
