Imports Contemn.Business
Imports TGGD.UI

Friend Class KeyCommandBindingState
    Inherits PickerState
    Private Shared ReadOnly BIND_CHOICE As String = NameOf(BIND_CHOICE)
    Private Const BIND_TEXT = "Bind..."
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
            $"Keys For {command}",
            Hue.Brown,
            GenerateMenuItems(settings, command),
            GO_BACK_IDENTIFIER)
        Me.command = command
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings, command As String) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT),
                (BIND_CHOICE, BIND_TEXT)
            }
        For Each key In settings.KeyBindings.BoundKeys(command).Order()
            result.Add((key, key))
        Next
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New KeyBindingState(Buffer, World, Settings, command)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case BIND_CHOICE
                Return New BindKeyCommandState(Buffer, World, Settings, command)
            Case Else
                Return New UnbindKeyCommandState(Buffer, World, Settings, command, identifier)
        End Select
    End Function
End Class
