Public Interface ISettings
    Property Quit As Boolean
    Property SfxVolume As Single
    Property MuxVolume As Single
    Property Zoom As Integer
    ReadOnly Property HasSettings As Boolean
    ReadOnly Property ScreenWidth As Integer
    ReadOnly Property ScreenHeight As Integer
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
End Interface
