Imports Contemn.Business
Imports TGGD.UI

Friend Class RemoveKeyCommandState
    Inherits PickerState
    Private ReadOnly command As String
    Private ReadOnly key As String
    Private Shared ReadOnly UNBIND_IDENTIFIER As String = NameOf(UNBIND_IDENTIFIER)
    Const UNBIND_TEXT = "Unbind"
    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings,
                  command As String,
                  key As String)
        MyBase.New(
            buffer,
            world,
            settings,
            $"Remove Key For {command}...",
            Hue.Brown,
            GenerateMenuItems(settings, command),
            GO_BACK_IDENTIFIER)
        Me.command = command
        Me.key = key
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings, command As String) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT)
            }
        If settings.KeyBindings.BoundKeys(command).Count > 1 Then
            result.Add((UNBIND_IDENTIFIER, UNBIND_TEXT))
        End If
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New KeyCommandBindingState(Buffer, World, Settings, command)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case UNBIND_IDENTIFIER
                Settings.KeyBindings.Unbind(key)
                Settings.KeyBindings.Update()
                Return HandleCancel()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
