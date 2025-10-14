Imports Contemn.Business
Imports Contemn.Data
Imports TGGD.Business
Imports TGGD.UI

Public Class UIContext
    Implements IUIContext
    Implements IPlatform
    Protected ReadOnly buffer As IUIBuffer(Of Integer)
    Private state As IUIState = Nothing
    Protected ReadOnly settings As ISettings
    Private ReadOnly eventQueue As New Queue(Of IEnumerable(Of String))
    Private ReadOnly worldData As New WorldData
    Private ReadOnly Property World As IWorld
        Get
            Return New Business.World(worldData, AddressOf PlaySfx, Me)
        End Get
    End Property
    Const EVENT_PLAY_SFX = "PlaySfx"
    Private Sub PlaySfx(sfx As String)
        eventQueue.Enqueue({EVENT_PLAY_SFX, sfx})
    End Sub
    Sub New(columns As Integer, rows As Integer, frameBuffer As Integer(), settings As ISettings)
        Me.buffer = New UIBuffer(Of Integer)(columns, rows, frameBuffer)
        Me.settings = settings
        state = New TitleState(buffer, World, AddressOf PlaySfx, settings)
    End Sub

    Public ReadOnly Property [Event] As IEnumerable(Of String) Implements IUIContext.Event
        Get
            Return If(eventQueue.Any, eventQueue.Peek, Nothing)
        End Get
    End Property

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
