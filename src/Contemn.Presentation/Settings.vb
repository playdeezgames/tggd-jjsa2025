Imports Contemn.UI

Friend Class Settings
    Implements ISettings

    Sub New()
        Quit = False
        SfxVolume = 0.5
        MuxVolume = 0.5
    End Sub

    Public Property Quit As Boolean Implements ISettings.Quit

    Public Property SfxVolume As Single Implements ISettings.SfxVolume

    Public Property MuxVolume As Single Implements ISettings.MuxVolume

    Friend Shared Function Load(settingsFilename As String) As ISettings
        Return New Settings
    End Function
End Class
