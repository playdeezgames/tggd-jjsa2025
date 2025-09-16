Imports Contemn.Business
Imports TGGD.UI

Friend Class DeadState
    Inherits BaseState

    Public Sub New(buffer As IUIBuffer(Of Integer), world As Business.IWorld, playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill
        Dim y = (Buffer.Rows) \ 2
        Buffer.WriteCentered(y, "Yer Dead!", Hue.Red, Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Return New MainMenuState(Buffer, World, PlaySfx)
    End Function
End Class
