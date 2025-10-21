Imports Contemn.Business
Imports TGGD.UI

Friend Class ViewState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(buffer, world, settings)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill
        RenderMap()
    End Sub
    Private Sub RenderMap()
        Dim map = World.Avatar.Map
        Dim viewColumn = World.GetStatistic(StatisticType.ViewColumn)
        Dim viewRow = World.GetStatistic(StatisticType.ViewRow)
        For Each columnOffset In Enumerable.Range(-VIEW_WIDTH \ 2, VIEW_WIDTH)
            Dim column = viewColumn + columnOffset
            Dim displayColumn = VIEW_WIDTH \ 2 + columnOffset
            For Each rowOffset In Enumerable.Range(-VIEW_HEIGHT \ 2, VIEW_HEIGHT)
                Dim row = viewRow + rowOffset
                Dim displayRow = VIEW_HEIGHT \ 2 + rowOffset
                Dim location = map.GetLocation(column, row)
                RenderLocation(displayColumn, displayRow, location, column = viewColumn AndAlso row = viewRow)
            Next
        Next
    End Sub

    Private Sub RenderLocation(displayColumn As Integer, displayRow As Integer, location As ILocation, invert As Boolean)
        If location Is Nothing Then
            Buffer.Fill(displayColumn, displayRow, 1, 1, character:=&HB0, Hue.Cyan, Hue.Black)
        ElseIf location.HasCharacter Then
            Buffer.SetPixel(displayColumn, displayRow, location.Character.ToPixel(invert))
        ElseIf location.HasItems Then
            Buffer.SetPixel(displayColumn, displayRow, location.Items.First().ToPixel(invert))
        Else
            Buffer.SetPixel(displayColumn, displayRow, location.ToPixel(invert))
        End If
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Select Case command
            Case UI.Command.Red
                World.SetTag(TagType.ViewMode, False)
                Return New GameMenuState(Buffer, World, Settings)
            Case UI.Command.Up
                Return HandleMove(0, -1)
            Case UI.Command.Down
                Return HandleMove(0, 1)
            Case UI.Command.Left
                Return HandleMove(-1, 0)
            Case UI.Command.Right
                Return HandleMove(1, 0)
            Case Else
                Return Me
        End Select
    End Function

    Private Function HandleMove(deltaX As Integer, deltaY As Integer) As IUIState
        World.ChangeStatistic(StatisticType.ViewColumn, deltaX)
        World.ChangeStatistic(StatisticType.ViewRow, deltaY)
        Return Me
    End Function
End Class
