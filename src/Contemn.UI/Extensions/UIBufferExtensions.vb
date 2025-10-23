Imports System.Runtime.CompilerServices
Imports TGGD.UI
Imports Contemn.Business

Friend Module UIBufferExtensions
    Public Const CharacterCount = 256
    'Valid use of <Extension> because IUIBuffer should not know about characters, foreground, or background colors.
    <Extension>
    Friend Sub Fill(
                   buffer As IUIBuffer(Of Integer),
                   character As Byte,
                   foregroundColor As Integer,
                   backgroundColor As Integer,
                   invert As Boolean)
        buffer.Fill(
            0, 0,
            buffer.Columns, buffer.Rows,
            ToPixel(character, foregroundColor, backgroundColor, invert))
    End Sub
    'Valid use of <Extension> because IUIBuffer should not know about characters, foreground, or background colors.
    <Extension>
    Friend Sub Fill(
                   buffer As IUIBuffer(Of Integer),
                   column As Integer,
                   row As Integer,
                   columns As Integer,
                   rows As Integer,
                   character As Byte,
                   foregroundColor As Integer,
                   backgroundColor As Integer,
                   invert As Boolean)
        buffer.Fill(
            column, row,
            columns, rows,
            ToPixel(character, foregroundColor, backgroundColor, invert))
    End Sub


    Friend Function ToPixel(
                            character As Byte,
                            foregroundColor As Integer,
                            backgroundColor As Integer,
                            invert As Boolean) As Integer
        Return character + If(invert, backgroundColor, foregroundColor) * CharacterCount + If(invert, foregroundColor, backgroundColor) * HueCount * CharacterCount
    End Function
    'Valid use of <Extension> because IUIBuffer should not know about characters, foreground, or background colors.
    <Extension>
    Friend Sub Write(
                    buffer As IUIBuffer(Of Integer),
                    column As Integer, row As Integer,
                    text As String,
                    foregroundColor As Integer,
                    backgroundColor As Integer,
                    invert As Boolean)
        For Each character In text
            buffer.SetPixel(column, row, ToPixel(CByte(AscW(character)), foregroundColor, backgroundColor, invert))
            column += 1
        Next
    End Sub
    'Valid use of <Extension> because IUIBuffer should not know about characters, foreground, or background colors.
    <Extension>
    Friend Sub WriteCentered(
                    buffer As IUIBuffer(Of Integer),
                    row As Integer,
                    text As String,
                    foregroundColor As Integer,
                    backgroundColor As Integer,
                    invert As Boolean)
        buffer.Write((buffer.Columns - text.Length) \ 2, row, text, foregroundColor, backgroundColor, invert)
    End Sub
End Module
