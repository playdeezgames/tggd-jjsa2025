Imports Contemn.Business
Imports TGGD.UI

Friend Class MuxVolumeSettingsState
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
            }.Concat(Enumerable.Range(0, 11).Select(Function(x) (x.ToString(), $"{x * 10}%")))
    End Function

    Private Shared Function GenerateTitle(settings As ISettings) As String
        Return $"MUX Volume({CInt(settings.MuxVolume * 100)}%)"
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case LEAVE_IT_IDENTIFIER
                Return New SettingsState(Buffer, World, Settings)
            Case Else
                Settings.MuxVolume = CSng(CInt(identifier) / 10)
                Return New MuxVolumeSettingsState(Buffer, World, Settings)
        End Select
    End Function
End Class
