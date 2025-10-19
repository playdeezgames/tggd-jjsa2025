Imports Contemn.Business
Imports TGGD.UI

Friend Class KeyCommandBindingState
    Inherits PickerState
    Private Shared ReadOnly ADD_CHOICE As String = NameOf(ADD_CHOICE)
    Private Const ADD_TEXT = "Add..."
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
                (ADD_CHOICE, ADD_TEXT)
            }
        For Each key In settings.BoundKeys(command)
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
            Case ADD_CHOICE
                Return New AddKeyCommandState(Buffer, World, Settings, command)
            Case Else
                Return New RemoveKeyCommandState(Buffer, World, Settings, command, identifier)
        End Select
    End Function
End Class
