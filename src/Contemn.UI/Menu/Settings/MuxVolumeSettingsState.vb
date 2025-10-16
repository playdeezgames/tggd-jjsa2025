Imports Contemn.Business
Imports TGGD.UI

Friend Class MuxVolumeSettingsState
    Inherits PickerState
    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            GenerateTitle(settings),
            Hue.Brown,
            GenerateMenuItems(settings),
            CInt(10 * settings.MuxVolume).ToString)
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Return Enumerable.Range(0, 11).Select(Function(x) (x.ToString(), $"{x * 10}%"))
    End Function

    Private Shared Function GenerateTitle(settings As ISettings) As String
        Return $"MUX Volume({CInt(settings.MuxVolume * 100)}%)"
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings, SettingsState.MUX_VOLUME_IDENTIFIER)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case Else
                Dim newVolume = CSng(CInt(identifier) / 10)
                If newVolume = Settings.MuxVolume Then
                    Return HandleCancel()
                Else
                    Settings.MuxVolume = newVolume
                    Return New MuxVolumeSettingsState(Buffer, World, Settings)
                End If
        End Select
    End Function
End Class
