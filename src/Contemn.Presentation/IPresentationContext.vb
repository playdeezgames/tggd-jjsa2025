Imports TGGD.Business
Imports TGGD.UI

Public Interface IPresentationContext
    Inherits IUIContext
    Inherits IPlatform
    Sub SetSizeHook(value As Action(Of (Integer, Integer), Boolean))
    Sub SetMuxVolumeHook(value As Action(Of Single))
    Sub SetSfxHook(value As Action(Of String))
    Sub SetMuxHook(value As Action(Of String))
    Sub Update(elapsedGameTime As TimeSpan)
    Sub Render(displayBuffer As IDisplayBuffer)
    Sub SetCommandTableHook(value As Action)
    ReadOnly Property Size As (Integer, Integer)
    ReadOnly Property FullScreen As Boolean
    ReadOnly Property SfxVolume As Single
    ReadOnly Property MuxVolume As Single
    ReadOnly Property QuitRequested As Boolean
    ReadOnly Property KeysFilename As String
End Interface
