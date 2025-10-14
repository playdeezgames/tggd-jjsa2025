Imports Contemn.UI

Friend Class Settings
    Implements ISettings

    Public Sub SignalQuit() Implements ISettings.SignalQuit
        Throw New NotImplementedException()
    End Sub

    Friend Shared Function Load(settingsFilename As String) As ISettings
        Return New Settings
    End Function
End Class
