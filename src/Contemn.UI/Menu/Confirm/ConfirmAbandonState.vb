Imports TGGD.UI
Imports Contemn.Business
Imports System.IO

Friend Class ConfirmAbandonState
    Inherits ConfirmState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Confirm Abandon?",
            Hue.Red)
    End Sub

    Protected Overrides Function OnCancel() As IUIState
        Return New GameMenuState(Buffer, World, Settings)
    End Function

    Protected Overrides Function OnConfirm() As IUIState
        If Settings.HasSettings Then
            File.Delete(World.GetMetadata(MetadataType.SaveSlot))
        End If
        World.Clear()
        Return New MainMenuState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New GameMenuState(Buffer, World, Settings)
    End Function
End Class
