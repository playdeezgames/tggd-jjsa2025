Imports Contemn.UI

Friend Class Settings
    Implements ISettings

    Sub New()
        Quit = False
        SfxVolume = 0.5
    End Sub

    Public Property Quit As Boolean Implements ISettings.Quit

    Public Property SfxVolume As Single Implements ISettings.SfxVolume

    Friend Shared Function Load(settingsFilename As String) As ISettings
        Return New Settings
    End Function
End Class
