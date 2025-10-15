Imports Contemn.Business
Imports TGGD.UI

Friend Class WindowSizeSettingsState
    Inherits PickerState
    Shared ReadOnly LEAVE_IT_IDENTIFIER As String = NameOf(LEAVE_IT_IDENTIFIER)
    Const LEAVE_IT_TEXT = "Leave It!"
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
            GenerateMenuItems(settings))
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Return {
                (LEAVE_IT_IDENTIFIER, LEAVE_IT_TEXT)
            }.Concat(Enumerable.Range(1, 10).Select(Function(x) (x.ToString(), $"{x * settings.ViewWidth}x{x * settings.ViewHeight}")))
    End Function

    Private Shared Function GenerateTitle(settings As ISettings) As String
        Return $"Window Size({settings.ScreenWidth}x{settings.ScreenHeight})"
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case LEAVE_IT_IDENTIFIER
                Return New SettingsState(Buffer, World, Settings)
            Case Else
                Settings.Zoom = CInt(identifier)
                Return New WindowSizeSettingsState(Buffer, World, Settings)
        End Select
    End Function
End Class
