Imports Contemn.Business
Imports TGGD.UI

Friend Class SfxVolumeSettingsState
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
            CInt(10 * settings.SfxVolume).ToString)
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Return Enumerable.Range(0, 11).Select(Function(x) (x.ToString(), $"{x * 10}%"))
    End Function

    Private Shared Function GenerateTitle(settings As ISettings) As String
        Return $"SFX Volume({CInt(settings.SfxVolume * 100)}%)"
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings, SettingsState.SFX_VOLUME_IDENTIFIER)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case Else
                Dim newVolume = CSng(CInt(identifier) / 10)
                If Settings.SfxVolume = newVolume Then
                    Return New SettingsState(Buffer, World, Settings, SettingsState.SFX_VOLUME_IDENTIFIER)
                Else
                    Settings.SfxVolume = newVolume
                    World.Platform.PlaySfx(Sfx.WooHoo)
                    Return New SfxVolumeSettingsState(Buffer, World, Settings)
                End If
        End Select
    End Function
End Class
