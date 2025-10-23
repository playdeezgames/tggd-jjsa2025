Imports System.IO
Imports System.Text.Json
Imports Contemn.Business
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media
Imports TGGD.UI

Public Class Host
    Inherits Game
    Private ReadOnly _controller As IPresentationContext
    Private ReadOnly _viewSize As (Integer, Integer)
    Private ReadOnly _graphics As GraphicsDeviceManager
    Private ReadOnly _hueTable As IReadOnlyDictionary(Of Integer, Color)
    Private _texture As Texture2D
    Private _spriteBatch As SpriteBatch
    Private _displayBuffer As IDisplayBuffer
    Private _commandTable As IReadOnlyDictionary(Of String, Func(Of KeyboardState, GamePadState, Boolean))
    Private ReadOnly _sfxSoundEffects As New Dictionary(Of String, SoundEffect)
    Private ReadOnly _sfxFilenames As IReadOnlyDictionary(Of String, String)
    Private ReadOnly _muxSongs As New Dictionary(Of String, Song)
    Private ReadOnly _muxFilenames As IReadOnlyDictionary(Of String, String)
    Private ReadOnly _title As String
    Private _currentMux As String = Nothing
    Sub New(
           title As String,
           controller As IPresentationContext,
           viewSize As (Integer, Integer),
           hueTable As IReadOnlyDictionary(Of Integer, Color),
           sfxFileNames As IReadOnlyDictionary(Of String, String),
           muxFileNames As IReadOnlyDictionary(Of String, String))
        _title = title
        _graphics = New GraphicsDeviceManager(Me)
        _controller = controller
        _viewSize = viewSize

        _sfxFilenames = sfxFileNames
        _muxFilenames = muxFileNames
        _hueTable = hueTable
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub Initialize()
        UpdateCommandTable()
        _controller.SetSizeHook(AddressOf OnWindowSizeChange)
        _controller.SetMuxVolumeHook(AddressOf OnMuxVolumeChange)
        Window.Title = _title
        OnWindowSizeChange(_controller.Size, _controller.FullScreen)
        For Each entry In _sfxFilenames
            _sfxSoundEffects(entry.Key) = SoundEffect.FromFile(entry.Value)
        Next
        For Each entry In _muxFilenames
            _muxSongs(entry.Key) = Song.FromUri(entry.Key, New Uri(entry.Value, UriKind.Relative))
        Next
        _controller.SetSfxHook(AddressOf OnSfx)
        _controller.SetMuxHook(AddressOf OnMux)
        _controller.SetCommandTableHook(AddressOf UpdateCommandTable)
        OnMux(Mux.Song)
        MediaPlayer.IsRepeating = True
        MyBase.Initialize()
    End Sub

    Private Sub UpdateCommandTable()
        _commandTable = LoadCommands()
    End Sub

    Private Sub OnMuxVolumeChange(volume As Single)
        MediaPlayer.Volume = volume
    End Sub

    Private Sub OnWindowSizeChange(newSize As (Integer, Integer), fullScreen As Boolean)
        _graphics.PreferredBackBufferWidth = newSize.Item1
        _graphics.PreferredBackBufferHeight = newSize.Item2
        _graphics.IsFullScreen = fullScreen
        _graphics.ApplyChanges()
    End Sub
    Const Pitch = 0.0F
    Const Pan = 0.0F
    Private Sub OnSfx(sfx As String)
        If sfx IsNot Nothing AndAlso _sfxSoundEffects.ContainsKey(sfx) Then
            _sfxSoundEffects(sfx).Play(_controller.SfxVolume, Pitch, Pan)
        End If
    End Sub
    Private Sub OnMux(mux As String)
        MediaPlayer.Volume = _controller.MuxVolume
        If mux IsNot Nothing AndAlso mux <> _currentMux AndAlso _muxFilenames.ContainsKey(mux) Then
            MediaPlayer.Play(_muxSongs(mux))
            _currentMux = mux
        End If
    End Sub
    Protected Overrides Sub LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _viewSize.Item1, _viewSize.Item2)
        _displayBuffer = New DisplayBuffer(_texture, _hueTable)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        UpdateKeyboardState()
        If _controller.QuitRequested Then
            Me.Exit()
            Return
        End If
        _controller.Update(gameTime.ElapsedGameTime)
        _controller.Render(_displayBuffer)
        _displayBuffer.Commit()
        MyBase.Update(gameTime)
    End Sub
    Private Sub UpdateKeyboardState()
        Dim newState = Keyboard.GetState()
        For Each cmd In CommandTransformer(Keyboard.GetState(), GamePad.GetState(PlayerIndex.One))
            _controller.HandleCommand(cmd)
        Next
    End Sub
    Private Function CommandTransformer(keyboard As KeyboardState, gamePad As GamePadState) As String()
        Dim result As New HashSet(Of String)
        For Each entry In _commandTable
            CheckForCommands(result, entry.Value(keyboard, gamePad), entry.Key)
        Next
        Return result.ToArray
    End Function
    Private ReadOnly _nextCommandTimes As New Dictionary(Of String, DateTimeOffset)
    Private Sub CheckForCommands(commands As HashSet(Of String), isPressed As Boolean, command As String)
        If isPressed Then
            If _nextCommandTimes.ContainsKey(command) Then
                If DateTimeOffset.Now > _nextCommandTimes(command) Then
                    commands.Add(command)
                    _nextCommandTimes(command) = DateTimeOffset.Now.AddSeconds(0.3)
                End If
            Else
                commands.Add(command)
                _nextCommandTimes(command) = DateTimeOffset.Now.AddSeconds(1.0)
            End If
        Else
            _nextCommandTimes.Remove(command)
        End If
    End Sub
    Const Zero = 0
    Protected Overrides Sub Draw(gameTime As GameTime)
        _graphics.GraphicsDevice.Clear(Color.Black)
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        _spriteBatch.Draw(_texture, New Rectangle(Zero, Zero, _controller.Size.Item1, _controller.Size.Item2), Nothing, Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub

    Private Function LoadCommands() As IReadOnlyDictionary(Of String, Func(Of KeyboardState, GamePadState, Boolean))
        Dim keysTable = JsonSerializer.Deserialize(Of Dictionary(Of Keys, String))(File.ReadAllText(_controller.KeysFilename))
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
    Private ReadOnly gamePadCommandTable As IReadOnlyDictionary(Of String, Func(Of GamePadState, Boolean)) =
    New Dictionary(Of String, Func(Of GamePadState, Boolean)) From
    {
        {UI.Command.GREEN, Function(gamePad) gamePad.IsButtonDown(Buttons.A)},
        {UI.Command.RED, Function(gamePad) gamePad.IsButtonDown(Buttons.B)},
        {UI.Command.UP, Function(gamePad) gamePad.DPad.Up = ButtonState.Pressed},
        {UI.Command.Down, Function(gamePad) gamePad.DPad.Down = ButtonState.Pressed},
        {UI.Command.LEFT, Function(gamePad) gamePad.DPad.Left = ButtonState.Pressed},
        {UI.Command.RIGHT, Function(gamePad) gamePad.DPad.Right = ButtonState.Pressed}
    }

    Private Function MakeCommandHandler(keys As List(Of Keys), func As Func(Of GamePadState, Boolean)) As Func(Of KeyboardState, GamePadState, Boolean)
        Return Function(k, g)
                   Return func(g) OrElse keys.Any(Function(x) k.IsKeyDown(x))
               End Function
    End Function

End Class
