Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module ItemExtensions
    Private ReadOnly itemPixelTable As IReadOnlyDictionary(Of String, Func(Of IItem, Integer)) =
        New Dictionary(Of String, Func(Of IItem, Integer)) From
        {
        }

    <Extension>
    Friend Function ToPixel(item As IItem) As Integer
        Dim pixelFunction As Func(Of IItem, Integer) = Nothing
        If itemPixelTable.TryGetValue(item.ItemType, pixelFunction) Then
            Return pixelFunction(item)
        End If
        Return UIBufferExtensions.ToPixel(Asc("?"), Hue.DarkGray, Hue.Black)
    End Function

End Module
