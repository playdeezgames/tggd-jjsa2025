Imports TGGD.UI
Imports Contemn.Business

Friend Class ConfirmAbandonState
    Inherits ConfirmState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(
            buffer,
            world,
            playSfx,
            "Confirm Abandon?",
            Hue.Red)
    End Sub

    Protected Overrides Function OnCancel() As IUIState
        Return New GameMenuState(Buffer, World, PlaySfx)
    End Function

    Protected Overrides Function OnConfirm() As IUIState
        World.Clear()
        Return New MainMenuState(Buffer, World, PlaySfx)
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New GameMenuState(Buffer, World, PlaySfx)
    End Function
End Class
