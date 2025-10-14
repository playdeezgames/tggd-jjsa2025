Imports Contemn.Business
Imports TGGD.UI

Friend Class SettingsState
    Inherits PickerState
    Private Shared ReadOnly GO_BACK_IDENTIFIER As String = NameOf(GO_BACK_IDENTIFIER)
    Const GO_BACK_TEXT = "Go Back"
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
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT)
            })
    End Sub

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
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
