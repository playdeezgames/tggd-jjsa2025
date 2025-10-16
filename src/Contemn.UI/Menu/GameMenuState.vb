Imports TGGD.UI
Imports Contemn.Business
Imports System.Runtime.CompilerServices

Friend Class GameMenuState
    Inherits PickerState

    Shared ReadOnly CONTINUE_IDENTIFIER As String = NameOf(CONTINUE_IDENTIFIER)
    Const CONTINUE_TEXT = "Continue"
    Shared ReadOnly ABANDON_IDENTIFIER As String = NameOf(ABANDON_IDENTIFIER)
    Const ABANDON_TEXT = "Abandon"
    Shared ReadOnly SETTINGS_IDENTIFIER As String = NameOf(SETTINGS_IDENTIFIER)
    Const SETTINGS_TEXT = "Settings"
    Shared ReadOnly SAVE_AND_CONTINUE_IDENTIFIER As String = NameOf(SAVE_AND_CONTINUE_IDENTIFIER)
    Const SAVE_AND_CONTINUE_TEXT = "Save and Continue"
    Shared ReadOnly QUICK_LOAD_IDENTIFIER As String = NameOf(QUICK_LOAD_IDENTIFIER)
    Const QUICK_LOAD_TEXT = "Quick Load"
    Shared ReadOnly SAVE_AND_EXIT_IDENTIFIER As String = NameOf(SAVE_AND_EXIT_IDENTIFIER)
    Const SAVE_AND_EXIT_TEXT = "Save and Exit"

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
        If settings.HasSettings Then
            result.Add((SETTINGS_IDENTIFIER, SETTINGS_TEXT))
            result.Add((SAVE_AND_CONTINUE_IDENTIFIER, SAVE_AND_CONTINUE_TEXT))
            result.Add((SAVE_AND_EXIT_IDENTIFIER, SAVE_AND_EXIT_TEXT))
            result.Add((QUICK_LOAD_IDENTIFIER, QUICK_LOAD_TEXT))
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
            Case SAVE_AND_EXIT_IDENTIFIER
                World.AddMessage(MoodType.Info, $"Saved game {DateTime.Now}")
                World.Save()
                Return New MainMenuState(Buffer, World, Settings)
            Case SAVE_AND_CONTINUE_IDENTIFIER
                World.AddMessage(MoodType.Info, $"Saved game {DateTime.Now}")
                World.Save()
                Return NeutralState.DetermineState(Buffer, World, Settings)
            Case QUICK_LOAD_IDENTIFIER
                Dim nextWorld = Business.World.Load(World.GetMetadata(MetadataType.SaveSlot), World.Platform)
                nextWorld.AddMessage(MoodType.Info, $"Quick Load {DateTime.Now}")
                Return NeutralState.DetermineState(Buffer, nextWorld, Settings)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function
End Class
