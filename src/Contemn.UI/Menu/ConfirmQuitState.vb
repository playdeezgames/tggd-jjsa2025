Imports Contemn.Business
Imports TGGD.UI

Friend Class ConfirmQuitState
    Inherits ConfirmState

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
            "Are you sure you want to quit?",
            Hue.Red)
    End Sub

    Protected Overrides Function OnCancel() As IUIState
        Return HandleCancel()
    End Function

    Protected Overrides Function OnConfirm() As IUIState
        Return New MainMenuState(Buffer, World, PlaySfx, Settings)
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New MainMenuState(Buffer, World, PlaySfx, Settings)
    End Function
End Class
