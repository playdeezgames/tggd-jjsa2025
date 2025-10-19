Imports System.IO
Imports Contemn.Business
Imports TGGD.UI

Friend Class ConfirmDeleteSlotState
    Inherits ConfirmState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Delete Save Slot?",
            Hue.Red)
    End Sub

    Protected Overrides Function OnCancel() As IUIState
        World.Clear()
        Return New MainMenuState(Buffer, World, Settings)
    End Function

    Protected Overrides Function OnConfirm() As IUIState
        File.Delete(World.GetMetadata(MetadataType.SaveSlot))
        World.Clear()
        Return New MainMenuState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return OnCancel()
    End Function
End Class
