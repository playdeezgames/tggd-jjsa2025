Imports Contemn.UI
Imports TGGD.UI

Public Class PresentationContext
    Inherits UIContext
    Implements IPresentationContext

    Private SizeHook As Action(Of (Integer, Integer), Boolean)
    Private MuxVolumeHook As Action(Of Single)
    Private SfxHook As Action(Of String)
    Private MuxHook As Action(Of String)

    Public Sub New(
                  columns As Integer,
                  rows As Integer,
                  frameBuffer() As Integer)
        MyBase.New(
            columns,
            rows,
            frameBuffer)
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
        Throw New NotImplementedException()
    End Sub

    Public Sub Render(displayBuffer As IDisplayBuffer) Implements IPresentationContext.Render
        Throw New NotImplementedException()
    End Sub
End Class
