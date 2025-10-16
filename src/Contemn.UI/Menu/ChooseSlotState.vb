Imports System.Linq.Expressions
Imports Contemn.Business
Imports TGGD.UI

Friend Class ChooseSlotState
    Inherits PickerState
    Private Shared ReadOnly GO_BACK_IDENTIFIER As String = NameOf(GO_BACK_IDENTIFIER)
    Const GO_BACK_TEXT = "Go Back"
    Const SLOT_COUNT = 5

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Choose Save Slot",
            Hue.Brown,
            GenerateMenuItems())
    End Sub

    Private Shared Function GenerateMenuItems() As IEnumerable(Of (Identifier As String, Text As String))
        Dim result As New List(Of (Identifier As String, Text As String)) From
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT)
            }
        For Each slot In Enumerable.Range(1, SLOT_COUNT)
            result.Add((slot.ToString, $"Slot {slot}"))
        Next
        Return result
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return New MainMenuState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case Else
                'TODO: check if the save slot already exists
                World.SetMetadata(MetadataType.SaveSlot, $"SaveSlot{identifier}.json")
                Return New EmbarkationState(Buffer, World, Settings)
        End Select
    End Function
End Class
