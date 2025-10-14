Imports TGGD.UI
Imports Contemn.Business

Friend Class GameMenuState
    Inherits PickerState

    Shared ReadOnly CONTINUE_IDENTIFIER As String = NameOf(CONTINUE_IDENTIFIER)
    Const CONTINUE_TEXT = "Continue"
    Shared ReadOnly ABANDON_IDENTIFIER As String = NameOf(ABANDON_IDENTIFIER)
    Const ABANDON_TEXT = "Abandon"
    Shared ReadOnly SETTINGS_IDENTIFIER As String = NameOf(SETTINGS_IDENTIFIER)
    Const SETTINGS_TEXT = "Settings"

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Game Menu",
            Hue.Magenta,
            GenerateMenuItems(settings))
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (CONTINUE_IDENTIFIER, CONTINUE_TEXT),
                (ABANDON_IDENTIFIER, ABANDON_TEXT)
            }
        If settings IsNot Nothing Then
            result.Add((SETTINGS_IDENTIFIER, SETTINGS_TEXT))
        End If
        Return result
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case CONTINUE_IDENTIFIER
                Return NeutralState.DetermineState(Buffer, World, Settings)
            Case ABANDON_IDENTIFIER
                Return New ConfirmAbandonState(Buffer, World, Settings)
            Case SETTINGS_IDENTIFIER
                Return New SettingsState(Buffer, World, Settings)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function
End Class
