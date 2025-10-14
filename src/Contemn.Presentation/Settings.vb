Imports Contemn.UI

Friend Class Settings
    Implements ISettings
    Friend Shared Function Load(settingsFilename As String) As ISettings
        Return New Settings
    End Function
End Class
