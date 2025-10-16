Imports Contemn.Business
Imports TGGD.UI

Friend Class WindowSizeSettingsState
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
            settings.Zoom.ToString)
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Return Enumerable.Range(1, 10).Select(Function(x) (x.ToString(), $"{x * settings.ViewWidth}x{x * settings.ViewHeight}"))
    End Function

    Private Shared Function GenerateTitle(settings As ISettings) As String
        Return $"Window Size({settings.ScreenWidth}x{settings.ScreenHeight})"
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings, SettingsState.WINDOW_SIZE_IDENTIFIER)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case Else
                Dim newZoom = CInt(identifier)
                If newZoom = Settings.Zoom Then
                    Return HandleCancel()
                Else
                    Settings.Zoom = newZoom
                    Return New WindowSizeSettingsState(Buffer, World, Settings)
                End If
        End Select
    End Function
End Class
