Imports Contemn.Business
Imports TGGD.UI

Friend Class KeyBindingState
    Inherits PickerState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Key Bindings",
            Hue.Brown,
            GenerateMenuItems(settings))
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT)
            }
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New SettingsState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Throw New NotImplementedException()
    End Function
End Class
