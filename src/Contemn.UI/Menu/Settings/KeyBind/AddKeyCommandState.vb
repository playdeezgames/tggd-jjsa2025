Imports Contemn.Business
Imports TGGD.UI

Friend Class AddKeyCommandState
    Inherits PickerState
    Private ReadOnly command As String

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings,
                  command As String)
        MyBase.New(
            buffer,
            world,
            settings,
            $"Add Key For {command}...",
            Hue.Brown,
            GenerateMenuItems(settings, command),
            GO_BACK_IDENTIFIER)
        Me.command = command
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings, command As String) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT)
            }
        For Each key In settings.AvailableKeys
            result.Add((key, key))
        Next
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New KeyCommandBindingState(Buffer, World, Settings, command)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case Else
                Settings.AddKey(command, identifier)
                Return HandleCancel()
        End Select
    End Function
End Class
