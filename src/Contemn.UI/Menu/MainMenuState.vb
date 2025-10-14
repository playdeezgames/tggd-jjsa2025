Imports TGGD.UI
Imports Contemn.Business

Friend Class MainMenuState
    Inherits PickerState
    Shared ReadOnly EMBARK_IDENTIFIER As String = NameOf(EMBARK_IDENTIFIER)
    Const EMBARK_TEXT = "Embark!"
    Shared ReadOnly ABOUT_IDENTIFIER As String = NameOf(ABOUT_IDENTIFIER)
    Const ABOUT_TEXT = "About"
    Shared ReadOnly SETTINGS_IDENTIFIER As String = NameOf(SETTINGS_IDENTIFIER)
    Const SETTINGS_TEXT = "Settings"
    Shared ReadOnly QUIT_IDENTIFIER As String = NameOf(QUIT_IDENTIFIER)
    Const QUIT_TEXT = "Quit"
    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Main Menu",
            Hue.Magenta,
            GenerateMenuItems(settings))
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (EMBARK_IDENTIFIER, EMBARK_TEXT),
                (ABOUT_IDENTIFIER, ABOUT_TEXT)
            }
        If settings.HasSettings Then
            result.Add((SETTINGS_IDENTIFIER, SETTINGS_TEXT))
            result.Add((QUIT_IDENTIFIER, QUIT_TEXT))
        End If
        Return result
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case EMBARK_IDENTIFIER
                Return HandleEmbarkation()
            Case ABOUT_IDENTIFIER
                Return New AboutState(Buffer, World, Settings)
            Case SETTINGS_IDENTIFIER
                Return New SettingsState(Buffer, World, Settings)
            Case QUIT_IDENTIFIER
                Return New ConfirmQuitState(Buffer, World, Settings)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function HandleEmbarkation() As IUIState
        Return New EmbarkationState(
            Buffer,
            World,
            Settings)
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return Me
    End Function
End Class
