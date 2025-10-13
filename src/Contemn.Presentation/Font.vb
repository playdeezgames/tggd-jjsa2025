Public Class Font
    Private ReadOnly _glyphs As New Dictionary(Of Integer, GlyphBuffer)
    Public ReadOnly Height As Integer
    Public ReadOnly Property HalfHeight As Integer
        Get
            Return Height \ 2
        End Get
    End Property
    Public Sub New(fontData As FontData)
        Height = fontData.Height
        For Each glyph In fontData.Glyphs.Keys
            _glyphs(glyph) = New GlyphBuffer(fontData, glyph)
        Next
    End Sub
    Public Sub WriteText(sink As IPixelSink, position As (Integer, Integer), character As Integer, hue As Integer)
        Dim buffer = _glyphs(character)
        buffer.CopyTo(sink, position, hue)
    End Sub
End Class
