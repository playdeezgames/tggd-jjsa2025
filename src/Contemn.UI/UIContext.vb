Imports Contemn.Business
Imports Contemn.Data
Imports TGGD.Business
Imports TGGD.UI

Public MustInherit Class UIContext
    Implements IUIContext
    Implements IPlatform
    Implements ISettings
    Protected ReadOnly buffer As IUIBuffer(Of Integer)
    Private state As IUIState = Nothing
    Private ReadOnly eventQueue As New Queue(Of IEnumerable(Of String))
    Private ReadOnly worldData As New WorldData
    Private ReadOnly Property World As IWorld
        Get
            Return New Business.World(worldData, Me)
        End Get
    End Property
    Const EVENT_PLAY_SFX = "PlaySfx"
    Public Sub PlaySfx(sfx As String) Implements IPlatform.PlaySfx
        eventQueue.Enqueue({EVENT_PLAY_SFX, sfx})
    End Sub
    Sub New(columns As Integer, rows As Integer, frameBuffer As Integer())
        Me.buffer = New UIBuffer(Of Integer)(columns, rows, frameBuffer)
        state = New TitleState(buffer, World, Me)
    End Sub

    Public ReadOnly Property [Event] As IEnumerable(Of String) Implements IUIContext.Event
        Get
            Return If(eventQueue.Any, eventQueue.Peek, Nothing)
        End Get
    End Property

    Public MustOverride Property Quit As Boolean Implements ISettings.Quit
    Public MustOverride Property SfxVolume As Single Implements ISettings.SfxVolume
    Public MustOverride Property MuxVolume As Single Implements ISettings.MuxVolume
    Public MustOverride ReadOnly Property HasSettings As Boolean Implements ISettings.HasSettings
    Public MustOverride Property Zoom As Integer Implements ISettings.Zoom
    Public MustOverride ReadOnly Property ScreenWidth As Integer Implements ISettings.ScreenWidth
    Public MustOverride ReadOnly Property ScreenHeight As Integer Implements ISettings.ScreenHeight
    Public MustOverride ReadOnly Property ViewWidth As Integer Implements ISettings.ViewWidth
    Public MustOverride ReadOnly Property ViewHeight As Integer Implements ISettings.ViewHeight
    Public MustOverride Property FullScreen As Boolean Implements ISettings.FullScreen
    Public MustOverride ReadOnly Property KeyBindings As IKeyBindings Implements ISettings.KeyBindings
    Public MustOverride ReadOnly Property IsDemo As Boolean Implements ISettings.IsDemo
    Public MustOverride ReadOnly Property Demo As Boolean Implements IPlatform.Demo

    Public Sub NextEvent() Implements IUIContext.NextEvent
        If eventQueue.Any Then
            eventQueue.Dequeue()
        End If
    End Sub

    Public Sub Refresh() Implements IUIContext.Refresh
        state.Refresh()
    End Sub

    Public Sub HandleCommand(command As String) Implements IUIContext.HandleCommand
        state = state.HandleCommand(command)
    End Sub
End Class
