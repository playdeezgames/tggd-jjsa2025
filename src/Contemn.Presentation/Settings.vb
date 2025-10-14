Imports Contemn.UI

Friend Class Settings
    Implements ISettings

    Sub New()
        Quit = False
    End Sub

    Public Property Quit As Boolean Implements ISettings.Quit

    Friend Shared Function Load(settingsFilename As String) As ISettings
        Return New Settings
    End Function
End Class
