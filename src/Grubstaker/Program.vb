Imports System.IO
Imports System.Text.Json
Imports Contemn.Business
Imports Contemn.Presentation
Imports Contemn.UI
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Module Program
    Sub Main(args As String())
        Dim frameBuffer = Enumerable.Range(0, VIEW_COLUMNS * VIEW_ROWS).Select(Function(x) 0).ToArray
        Using host As New Host(
            GAME_TITLE,
            New PresentationContext(frameBuffer, FontFilename),
            (VIEW_WIDTH, VIEW_HEIGHT),
            LoadHues(),
            LoadCommands(),
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

    Private Function LoadCommands() As IReadOnlyDictionary(Of String, Func(Of KeyboardState, GamePadState, Boolean))
        Dim keysTable = JsonSerializer.Deserialize(Of Dictionary(Of Keys, String))(File.ReadAllText(KeysFilename))
        Dim keysForCommands = keysTable.
            GroupBy(Function(x) x.Value).
            ToDictionary(
                Function(x) x.Key,
                Function(x) x.Select(Function(y) y.Key).ToList())
        Dim result = New Dictionary(Of String, Func(Of KeyboardState, GamePadState, Boolean))
        For Each cmd In gamePadCommandTable.Keys
            result.Add(cmd, MakeCommandHandler(If(keysForCommands.ContainsKey(cmd), keysForCommands(cmd), Array.Empty(Of Keys)().ToList), gamePadCommandTable(cmd)))
        Next
        Return result
    End Function

    Private Function MakeCommandHandler(keys As List(Of Keys), func As Func(Of GamePadState, Boolean)) As Func(Of KeyboardState, GamePadState, Boolean)
        Return Function(k, g)
                   Return func(g) OrElse keys.Any(Function(x) k.IsKeyDown(x))
               End Function
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
    Private ReadOnly gamePadCommandTable As IReadOnlyDictionary(Of String, Func(Of GamePadState, Boolean)) =
    New Dictionary(Of String, Func(Of GamePadState, Boolean)) From
    {
        {Command.Green, Function(gamePad) gamePad.IsButtonDown(Buttons.A)},
        {Command.Red, Function(gamePad) gamePad.IsButtonDown(Buttons.B)},
        {Command.Up, Function(gamePad) gamePad.DPad.Up = ButtonState.Pressed},
        {Command.Down, Function(gamePad) gamePad.DPad.Down = ButtonState.Pressed},
        {Command.Left, Function(gamePad) gamePad.DPad.Left = ButtonState.Pressed},
        {Command.Right, Function(gamePad) gamePad.DPad.Right = ButtonState.Pressed}
    }
End Module
