Imports TGGD.UI
Imports Contemn.Business

Friend Class GameMenuState
    Inherits PickerState

    Shared ReadOnly CONTINUE_IDENTIFIER As String = NameOf(CONTINUE_IDENTIFIER)
    Const CONTINUE_TEXT = "Continue"
    Shared ReadOnly ABANDON_IDENTIFIER As String = NameOf(ABANDON_IDENTIFIER)
    Const ABANDON_TEXT = "Abandon"

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(
            buffer,
            world,
            playSfx,
            "Game Menu",
            Hue.Magenta,
            {
                (CONTINUE_IDENTIFIER, CONTINUE_TEXT),
                (ABANDON_IDENTIFIER, ABANDON_TEXT)
            })
    End Sub

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case CONTINUE_IDENTIFIER
                Return NeutralState.DetermineState(Buffer, World, PlaySfx)
            Case ABANDON_IDENTIFIER
                Return New ConfirmAbandonState(Buffer, World, PlaySfx)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Protected Overrides Function HandleCancel() As IUIState
        Return NeutralState.DetermineState(Buffer, World, PlaySfx)
    End Function
End Class
