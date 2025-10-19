Imports Contemn.Business
Imports TGGD.UI

Friend Class KeyBindingState
    Inherits PickerState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings,
                  identifier As String)
        MyBase.New(
            buffer,
            world,
            settings,
            "Key Bindings",
            Hue.Brown,
            GenerateMenuItems(settings),
            identifier)
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT)
            }
        For Each command In settings.Commands
            result.Add((command, command))
        Next
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings, SettingsState.KEY_BINDING_IDENTIFIER)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case Else
                Return New KeyCommandBindingState(Buffer, World, Settings, identifier)
        End Select
    End Function
End Class
