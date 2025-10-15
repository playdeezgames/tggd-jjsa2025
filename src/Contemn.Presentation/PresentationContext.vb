Imports System.IO
Imports System.Text.Json
Imports Contemn.UI

Public Class PresentationContext
    Inherits UIContext
    Implements IPresentationContext

    Private SizeHook As Action(Of (Integer, Integer), Boolean)
    Private MuxVolumeHook As Action(Of Single)
    Private SfxHook As Action(Of String)
    Private MuxHook As Action(Of String)
    Private ReadOnly font As Font
    Private _muxVolume As Single
    Private _zoom As Integer
    Private _fullScreen As Boolean

    Public Sub New(frameBuffer() As Integer, fontFilename As String, settingsFilename As String)
        MyBase.New(
            VIEW_COLUMNS,
            VIEW_ROWS,
            frameBuffer)
        font = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(fontFilename)))
        Quit = False
        SfxVolume = 0.5
        _muxVolume = 0.1
        _zoom = 4
        _fullScreen = False
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

    Public Overrides Property MuxVolume As Single
        Get
            Return _muxVolume
        End Get
        Set(value As Single)
            _muxVolume = value
            MuxVolumeHook(value)
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
        End Set
    End Property

    Private ReadOnly Property IPresentationContext_FullScreen As Boolean Implements IPresentationContext.FullScreen
        Get
            Return FullScreen
        End Get
    End Property

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
End Class
