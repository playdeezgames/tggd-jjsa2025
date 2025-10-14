Imports Contemn.Business
Imports TGGD.UI

Friend Class SettingsState
    Inherits PickerState
    Private Shared ReadOnly GO_BACK_IDENTIFIER As String = NameOf(GO_BACK_IDENTIFIER)
    Const GO_BACK_TEXT = "Go Back"
    Private Shared ReadOnly SFX_VOLUME_IDENTIFIER As String = NameOf(SFX_VOLUME_IDENTIFIER)
    Const SFX_VOLUME_TEXT = "SFX Volume"
    Private Shared ReadOnly MUX_VOLUME_IDENTIFIER As String = NameOf(MUX_VOLUME_IDENTIFIER)
    Const MUX_VOLUME_TEXT = "MUX Volume"
    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String),
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            playSfx,
            settings,
            "Settings",
            Hue.Brown,
            GenerateMenuItems(settings))
    End Sub

    Private Shared Function GenerateMenuItems(settings As ISettings) As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT),
                (SFX_VOLUME_IDENTIFIER, $"{SFX_VOLUME_TEXT}({CInt(settings.SfxVolume * 100)}%)"),
                (MUX_VOLUME_IDENTIFIER, $"{MUX_VOLUME_TEXT}({CInt(settings.MuxVolume * 100)}%)")
            }
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        If World.Avatar IsNot Nothing Then
            Return New GameMenuState(Buffer, World, PlaySfx, Settings)
        End If
        Return New MainMenuState(Buffer, World, PlaySfx, Settings)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case SFX_VOLUME_IDENTIFIER
                Return New SfxVolumeSettingsState(Buffer, World, PlaySfx, Settings)
            Case MUX_VOLUME_IDENTIFIER
                Return New MuxVolumeSettingsState(Buffer, World, PlaySfx, Settings)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
