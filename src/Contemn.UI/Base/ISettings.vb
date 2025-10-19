Imports Contemn.Business

Public Interface ISettings
    Property Quit As Boolean
    Property SfxVolume As Single
    Property MuxVolume As Single
    Property Zoom As Integer
    Property FullScreen As Boolean
    ReadOnly Property HasSettings As Boolean
    ReadOnly Property ScreenWidth As Integer
    ReadOnly Property ScreenHeight As Integer
    ReadOnly Property ViewWidth As Integer
    ReadOnly Property ViewHeight As Integer
    ReadOnly Property Commands As IEnumerable(Of String)
    Function KeysForCommand(command As String) As IEnumerable(Of String)
    ReadOnly Property AvailableKeys As IEnumerable(Of String)
    Sub Unbind(key As String)
    Sub AddKey(command As String, identifier As String)
End Interface
