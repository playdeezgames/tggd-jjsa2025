Imports TGGD.UI
Imports Contemn.Business

Friend Class AboutState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(buffer, world, settings)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill(0, Hue.Black, Hue.Black, False)
        Buffer.WriteCentered(0, "About", Hue.Brown, Hue.Black, False)
        Dim y = 1
        Buffer.Write(0, y, "A production of TheGrumpyGameDev", Hue.LightGray, Hue.Black, False)
        y += 2
        Buffer.Write(0, y, "Originally Developed For:", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(0, y, "Jern Jam 2025 x SciAnts", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(0, y, "September 2025", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.WriteCentered(Buffer.Rows \ 2 + 5, "Winner: Uniqueness Award", Hue.LightGreen, Hue.Black, False)
        y += 2
        Buffer.Write(0, y, "Main Inspirations:", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(0, y, "IBM PS/2 Model 50 (SCREEN0:WIDTH40)", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(0, y, "Vintage Story", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(0, y, "One Hour, One Life", Hue.LightGray, Hue.Black, False)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Return New MainMenuState(Buffer, World, Settings)
    End Function
End Class
