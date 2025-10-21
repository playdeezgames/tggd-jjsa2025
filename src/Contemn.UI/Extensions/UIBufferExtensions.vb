Imports System.Runtime.CompilerServices
Imports TGGD.UI
Imports Contemn.Business

Friend Module UIBufferExtensions
    Public Const CharacterCount = 256
    <Extension>
    Friend Sub Fill(
                   buffer As IUIBuffer(Of Integer),
                   Optional character As Byte = 0,
                   Optional foregroundColor As Integer = 0,
                   Optional backgroundColor As Integer = 0,
                   Optional invert As Boolean = False)
        buffer.Fill(
            0, 0,
            buffer.Columns, buffer.Rows,
            ToPixel(character, foregroundColor, backgroundColor, invert))
    End Sub
    <Extension>
    Friend Sub Fill(
                   buffer As IUIBuffer(Of Integer),
                   column As Integer,
                   row As Integer,
                   columns As Integer,
                   rows As Integer,
                   Optional character As Byte = 0,
                   Optional foregroundColor As Integer = 0,
                   Optional backgroundColor As Integer = 0,
                   Optional invert As Boolean = False)
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

    <Extension>
    Friend Sub Write(
                    buffer As IUIBuffer(Of Integer),
                    column As Integer, row As Integer,
                    text As String,
                    foregroundColor As Integer,
                    backgroundColor As Integer,
                    Optional invert As Boolean = False)
        For Each character In text
            buffer.SetPixel(column, row, ToPixel(CByte(AscW(character)), foregroundColor, backgroundColor, invert))
            column += 1
        Next
    End Sub
    <Extension>
    Friend Sub WriteCentered(
                    buffer As IUIBuffer(Of Integer),
                    row As Integer,
                    text As String,
                    foregroundColor As Integer,
                    backgroundColor As Integer)
        buffer.Write((buffer.Columns - text.Length) \ 2, row, text, foregroundColor, backgroundColor)
    End Sub
End Module
