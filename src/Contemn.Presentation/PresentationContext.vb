Imports System.IO
Imports System.Text.Json
Imports Contemn.UI
Imports Microsoft.Xna.Framework.Input

Public Class PresentationContext
    Inherits UIContext
    Implements IPresentationContext
    Implements IKeyBindings
    Const SettingsFilename = "Content/Config/Settings.json"

    Private SizeHook As Action(Of (Integer, Integer), Boolean)
    Private MuxVolumeHook As Action(Of Single)
    Private SfxHook As Action(Of String)
    Private MuxHook As Action(Of String)
    Private ReadOnly font As Font
    Private _sfxVolume As Single
    Private _muxVolume As Single
    Private _zoom As Integer
    Private _fullScreen As Boolean
    Private CommandTableHook As Action
    Private ReadOnly keysFilename As String

    Public Sub New(frameBuffer() As Integer, fontFilename As String, settingsFilename As String, keysFilename As String)
        MyBase.New(
            VIEW_COLUMNS,
            VIEW_ROWS,
            frameBuffer)
        font = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(fontFilename)))
        Quit = False
        Me.keysFilename = keysFilename
        LoadInitialSettings()
    End Sub

    Private Sub LoadInitialSettings()
        Try
            Dim settingsData As SettingsData = JsonSerializer.Deserialize(Of SettingsData)(File.ReadAllText(SettingsFilename))
            _sfxVolume = settingsData.SfxVolume
            _muxVolume = settingsData.MuxVolume
            _zoom = settingsData.Zoom
            _fullScreen = settingsData.FullScreen
        Catch ex As Exception
            _sfxVolume = 0.5
            _muxVolume = 0.1
            _zoom = 4
            _fullScreen = False
        End Try
    End Sub

    Public ReadOnly Property Size As (Integer, Integer) Implements IPresentationContext.Size
        Get
            Return (VIEW_WIDTH * Zoom, VIEW_HEIGHT * Zoom)
        End Get
    End Property

    Private ReadOnly Property IPresentationContext_SfxVolume As Single Implements IPresentationContext.SfxVolume
        Get
            Return SfxVolume
        End Get
    End Property

    Private ReadOnly Property IPresentationContext_MuxVolume As Single Implements IPresentationContext.MuxVolume
        Get
            Return MuxVolume
        End Get
    End Property

    Public ReadOnly Property QuitRequested As Boolean Implements IPresentationContext.QuitRequested
        Get
            Return Me.Quit
        End Get
    End Property

    Public Overrides Property Quit As Boolean

    Public Overrides Property SfxVolume As Single
        Get
            Return _sfxVolume
        End Get
        Set(value As Single)
            _sfxVolume = value
            SaveSettings()
        End Set
    End Property

    Public Overrides Property MuxVolume As Single
        Get
            Return _muxVolume
        End Get
        Set(value As Single)
            _muxVolume = value
            MuxVolumeHook(value)
            SaveSettings()
        End Set
    End Property

    Public Overrides ReadOnly Property HasSettings As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Property Zoom As Integer
        Get
            Return _zoom
        End Get
        Set(value As Integer)
            _zoom = value
            SizeHook((ScreenWidth, ScreenHeight), FullScreen)
            SaveSettings()
        End Set
    End Property

    Public Overrides ReadOnly Property ScreenWidth As Integer
        Get
            Return VIEW_WIDTH * Zoom
        End Get
    End Property

    Public Overrides ReadOnly Property ScreenHeight As Integer
        Get
            Return VIEW_HEIGHT * Zoom
        End Get
    End Property

    Public Overrides ReadOnly Property ViewWidth As Integer
        Get
            Return VIEW_WIDTH
        End Get
    End Property

    Public Overrides ReadOnly Property ViewHeight As Integer
        Get
            Return VIEW_HEIGHT
        End Get
    End Property

    Public Overrides Property FullScreen As Boolean
        Get
            Return _fullScreen
        End Get
        Set(value As Boolean)
            _fullScreen = value
            SizeHook((ScreenWidth, ScreenHeight), FullScreen)
            SaveSettings()
        End Set
    End Property

    Private Sub SaveSettings()
        Dim settingsData As New SettingsData With
            {
                .FullScreen = _fullScreen,
                .MuxVolume = _muxVolume,
                .SfxVolume = _sfxVolume,
                .Zoom = _zoom
            }
        File.WriteAllText(SettingsFilename, JsonSerializer.Serialize(settingsData))
    End Sub

    Private ReadOnly Property IPresentationContext_FullScreen As Boolean Implements IPresentationContext.FullScreen
        Get
            Return FullScreen
        End Get
    End Property

    Public ReadOnly Property Commands As IEnumerable(Of String) Implements IKeyBindings.Commands
        Get
            Return LoadCommandKeys().Values.Distinct
        End Get
    End Property

    Public ReadOnly Property UnboundKeys As IEnumerable(Of String) Implements IKeyBindings.UnboundKeys
        Get
            Dim commandKeys = New HashSet(Of Keys)(LoadCommandKeys().Keys)
            Return [Enum].GetValues(Of Keys).Where(Function(x) x <> Keys.None AndAlso Not commandKeys.Contains(x)).Select(Function(x) x.ToString())
        End Get
    End Property

    Public Overrides ReadOnly Property KeyBindings As IKeyBindings
        Get
            Return Me
        End Get
    End Property

    Private ReadOnly Property IPresentationContext_KeysFilename As String Implements IPresentationContext.KeysFilename
        Get
            Return keysFilename
        End Get
    End Property

    Public Overrides ReadOnly Property IsDemo As Boolean
        Get
            Return False
        End Get
    End Property

    Private Function LoadCommandKeys() As Dictionary(Of Keys, String)
        Return JsonSerializer.Deserialize(Of Dictionary(Of Keys, String))(File.ReadAllText(keysFilename))
    End Function

    Public Sub SetSizeHook(value As Action(Of (Integer, Integer), Boolean)) Implements IPresentationContext.SetSizeHook
        Me.SizeHook = value
    End Sub

    Public Sub SetMuxVolumeHook(value As Action(Of Single)) Implements IPresentationContext.SetMuxVolumeHook
        Me.MuxVolumeHook = value
    End Sub

    Public Sub SetSfxHook(value As Action(Of String)) Implements IPresentationContext.SetSfxHook
        Me.SfxHook = value
    End Sub

    Public Sub SetMuxHook(value As Action(Of String)) Implements IPresentationContext.SetMuxHook
        Me.MuxHook = value
    End Sub

    Public Sub Update(elapsedGameTime As TimeSpan) Implements IPresentationContext.Update
        While [Event] IsNot Nothing
            HandleEvent([Event])
            NextEvent()
        End While
        Refresh()
    End Sub

    Private Sub HandleEvent([event] As IEnumerable(Of String))
        Select Case [event].First
            Case "PlaySfx"
                HandlePlaySfx([event].Skip(1))
        End Select
    End Sub

    Private Sub HandlePlaySfx(parameters As IEnumerable(Of String))
        SfxHook(parameters.First)
    End Sub

    Public Sub Render(displayBuffer As IDisplayBuffer) Implements IPresentationContext.Render
        For Each column In Enumerable.Range(0, VIEW_COLUMNS)
            For Each row In Enumerable.Range(0, VIEW_ROWS)
                Dim cell = buffer.GetPixel(column, row)
                Dim foreground = (cell \ 256) Mod 16
                Dim background = (cell \ 4096) Mod 16
                Dim character = cell Mod 256
                displayBuffer.Fill((column * CELL_WIDTH, row * CELL_HEIGHT), (CELL_WIDTH, CELL_HEIGHT), background)
                font.WriteText(displayBuffer, (column * CELL_WIDTH, row * CELL_HEIGHT), character, foreground)
            Next
        Next
    End Sub

    Public Function BoundKeys(command As String) As IEnumerable(Of String) Implements IKeyBindings.BoundKeys
        Return LoadCommandKeys().Where(Function(x) x.Value = command).Select(Function(x) x.Key.ToString)
    End Function

    Public Sub Unbind(key As String) Implements IKeyBindings.Unbind
        Dim commandKeys = LoadCommandKeys()
        commandKeys.Remove([Enum].Parse(Of Keys)(key))
        SaveCommandKeys(commandKeys)
    End Sub

    Private Sub SaveCommandKeys(commandKeys As Dictionary(Of Keys, String))
        File.WriteAllText(keysFilename, JsonSerializer.Serialize(commandKeys))
    End Sub

    Public Sub Bind(command As String, identifier As String) Implements IKeyBindings.Bind
        Dim commandKeys = LoadCommandKeys()
        commandKeys.Add([Enum].Parse(Of Keys)(identifier), command)
        SaveCommandKeys(commandKeys)
    End Sub

    Public Sub SetCommandTableHook(value As Action) Implements IPresentationContext.SetCommandTableHook
        Me.CommandTableHook = value
    End Sub

    Public Sub Update() Implements IKeyBindings.Update
        CommandTableHook()
    End Sub
End Class
