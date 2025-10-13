Imports System.Drawing
Imports System.IO
Imports System.Text.Json
Imports Contemn.Presentation

Module FontMaker
    Const InputFilename = "romfont8x8.png"
    Const CellWidth = 8
    Const CellHeight = 8
    Sub MakeFont()
        Dim bmp = New Bitmap(InputFilename)
        Dim rows = (bmp.Height + 1) \ (CellHeight)
        Dim columns = (bmp.Width + 1) \ (CellWidth)
        Dim glyph = 0
        Dim fontData As New FontData With {
            .Height = CellHeight,
            .Glyphs = New Dictionary(Of Integer, GlyphData)
        }
        For row = 0 To rows - 1
            For column = 0 To columns - 1
                Dim glyphData As New GlyphData With {.Width = CellWidth, .Lines = New Dictionary(Of Integer, IEnumerable(Of Integer))}
                fontData.Glyphs(glyph) = glyphData
                glyph += 1
                For y = 0 To CellHeight - 1
                    Dim line As New List(Of Integer)
                    For x = 0 To CellWidth - 1
                        Dim color = bmp.GetPixel(column * (CellWidth) + x, row * (CellHeight) + y)
                        If color.R = 0 AndAlso color.G = 0 AndAlso color.B = 0 Then
                            Console.Write(" ")
                        Else
                            Console.Write("#")
                            line.Add(x)
                        End If
                    Next
                    If line.Any Then
                        glyphData.Lines(y) = line
                    End If
                    Console.WriteLine()
                Next
            Next
        Next
        File.WriteAllText("output.json", JsonSerializer.Serialize(fontData))
    End Sub
End Module
