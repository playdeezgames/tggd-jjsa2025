Imports System.IO
Imports System.Text.Json
Imports Contemn.Business
Imports Contemn.Presentation
Imports Microsoft.Xna.Framework
Module Program
    Sub Main(args As String())
        Dim frameBuffer = Enumerable.Range(0, VIEW_COLUMNS * VIEW_ROWS).Select(Function(x) 0).ToArray
        Using host As New Host(
            GAME_TITLE,
            New PresentationContext(frameBuffer, FontFilename, SettingsFilename, KeysFilename, True),
            (VIEW_WIDTH, VIEW_HEIGHT),
            LoadHues(),
            LoadSfx(),
            LoadMux)
            host.Run()
        End Using
    End Sub

    Private Function LoadMux() As IReadOnlyDictionary(Of String, String)
        Return JsonSerializer.Deserialize(Of Dictionary(Of String, String))(File.ReadAllText(MuxFilename))
    End Function

    Private Function LoadSfx() As IReadOnlyDictionary(Of String, String)
        Return JsonSerializer.Deserialize(Of Dictionary(Of String, String))(File.ReadAllText(SfxFilename))
    End Function

    Private Function LoadHues() As IReadOnlyDictionary(Of Integer, Color)
        Return New Dictionary(Of Integer, Color) From
            {
                {Hue.Black, New Color(0, 0, 0)},
                {Hue.Blue, New Color(0, 0, 170)},
                {Hue.Green, New Color(0, 170, 0)},
                {Hue.Cyan, New Color(0, 170, 170)},
                {Hue.Red, New Color(170, 0, 0)},
                {Hue.Magenta, New Color(170, 0, 170)},
                {Hue.Brown, New Color(170, 85, 0)},
                {Hue.LightGray, New Color(170, 170, 170)},
                {Hue.DarkGray, New Color(85, 85, 85)},
                {Hue.LightBlue, New Color(85, 85, 255)},
                {Hue.LightGreen, New Color(85, 255, 85)},
                {Hue.LightCyan, New Color(85, 255, 255)},
                {Hue.LightRed, New Color(255, 85, 85)},
                {Hue.LightMagenta, New Color(255, 85, 255)},
                {Hue.Yellow, New Color(255, 255, 85)},
                {Hue.White, New Color(255, 255, 255)}
            }
    End Function
End Module
