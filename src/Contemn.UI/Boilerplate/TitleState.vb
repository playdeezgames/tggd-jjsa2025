Imports TGGD.UI
Imports Contemn.Business

Friend Class TitleState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill()
        Buffer.WriteCentered(Buffer.Rows \ 2, "PLACEHOLDER", Hue.LightCyan, Hue.Black)
        Buffer.WriteCentered(Buffer.Rows \ 2 + 2, "A Production of TheGrumpyGameDev", Hue.DarkGray, Hue.Black)
        Buffer.WriteCentered(Buffer.Rows \ 2 + 3, "For REASON(PROLLY A JAM)", Hue.DarkGray, Hue.Black)
        Buffer.WriteCentered(Buffer.Rows \ 2 + 4, "MONTH YEAR", Hue.DarkGray, Hue.Black)
        Buffer.WriteCentered(Buffer.Rows - 1, "Press <SPACE>", Hue.White, Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        If command = UI.Command.Green Then
            Return New MainMenuState(Buffer, World, PlaySfx)
        End If
        Return Me
    End Function
End Class
