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

    Public Sub New(frameBuffer() As Integer, fontFilename As String, settingsFilename As String)
        MyBase.New(
            VIEW_COLUMNS,
            VIEW_ROWS,
            frameBuffer,
            Presentation.Settings.Load(settingsFilename))
        Size = (VIEW_WIDTH * 4, VIEW_HEIGHT * 4)
        font = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(fontFilename)))
        SfxVolume = 1.0
    End Sub

    Public Property Size As (Integer, Integer) Implements IPresentationContext.Size

    Public Property FullScreen As Boolean Implements IPresentationContext.FullScreen

    Public Property SfxVolume As Single Implements IPresentationContext.SfxVolume

    Public Property MuxVolume As Single Implements IPresentationContext.MuxVolume

    Public Property QuitRequested As Boolean Implements IPresentationContext.QuitRequested

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
