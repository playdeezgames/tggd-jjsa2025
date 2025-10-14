Imports Contemn.Business
Imports TGGD.UI

Friend Class DeadState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(buffer, world, settings)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill
        Dim y = (Buffer.Rows) \ 2 - 2
        Buffer.WriteCentered(y, "Yer Dead!", Hue.Red, Hue.Black)
        y += 1
        Buffer.WriteCentered(y, World.Avatar.FormatStatistic(StatisticType.Score), Hue.Green, Hue.Black)
        y += 2
        Buffer.WriteCentered(y, "Winner: Nature", Hue.LightGreen, Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Return New MainMenuState(Buffer, World, Settings)
    End Function
End Class
