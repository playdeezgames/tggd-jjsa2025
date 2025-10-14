Imports TGGD.UI

Public Interface IPresentationContext
    Inherits IUIContext

    Sub SetSizeHook(value As Action(Of (Integer, Integer), Boolean))
    Sub SetMuxVolumeHook(value As Action(Of Single))
    Sub SetSfxHook(value As Action(Of String))
    Sub SetMuxHook(value As Action(Of String))
    Sub Update(elapsedGameTime As TimeSpan)
    Sub Render(displayBuffer As IDisplayBuffer)
    Property Size As (Integer, Integer)
    Property FullScreen As Boolean
    Property SfxVolume As Single
    Property MuxVolume As Single
    ReadOnly Property QuitRequested As Boolean
End Interface
