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
        Buffer.Fill(0, Hue.Black, Hue.Black, False)
        RenderMap()

        RenderQuickExamine()
        Buffer.WriteCentered(Buffer.Rows - 4, "(View Mode)", Hue.LightGray, Hue.Black, False)
        Buffer.WriteCentered(Buffer.Rows - 3, $"{Command.UP}/{Command.DOWN}/{Command.LEFT}/{Command.RIGHT}: move", Hue.LightGray, Hue.Black, False)
        Buffer.WriteCentered(Buffer.Rows - 2, $"{Command.GREEN}: examine", Hue.LightGray, Hue.Black, False)
        Buffer.WriteCentered(Buffer.Rows - 1, $"{Command.RED}: leave", Hue.LightGray, Hue.Black, False)
    End Sub

    Private Sub RenderQuickExamine()
        Dim location = World.Avatar.Location.Map.GetLocation(World.GetStatistic(StatisticType.ViewColumn), World.GetStatistic(StatisticType.ViewRow))
        If Not location.Visible Then
            Return
        End If
        Dim x = VIEW_WIDTH
        Dim y = 0
        Buffer.Write(x, y, $"Pos: ({World.GetStatistic(StatisticType.ViewColumn)}, {World.GetStatistic(StatisticType.ViewRow)})", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(x, y, $"Location Type:", Hue.LightGray, Hue.Black, False)
        y += 1
        Buffer.Write(x, y, location.Name, Hue.LightGray, Hue.Black, False)
        y += 1
        If location.Character IsNot Nothing Then
            Buffer.Write(x, y, $"Character:", Hue.LightGray, Hue.Black, False)
            y += 1
            Buffer.Write(x, y, location.Character.Name, Hue.LightGray, Hue.Black, False)
            y += 1
        End If
        If location.HasItems Then
            Buffer.Write(x, y, $"#Items: {location.Items.Count()}", Hue.LightGray, Hue.Black, False)
            y += 1
        End If
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
        If location Is Nothing OrElse Not location.Visible Then
            Buffer.Fill(displayColumn, displayRow, 1, 1, &HB0, Hue.Cyan, Hue.Black, invert)
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
            Case UI.Command.RED
                World.SetTag(TagType.ViewMode, False)
                Return NeutralState.DetermineState(Buffer, World, Settings)
            Case UI.Command.UP
                Return HandleMove(0, -1)
            Case UI.Command.Down
                Return HandleMove(0, 1)
            Case UI.Command.LEFT
                Return HandleMove(-1, 0)
            Case UI.Command.RIGHT
                Return HandleMove(1, 0)
            Case UI.Command.GREEN
                Return HandleExamine()
            Case Else
                Return Me
        End Select
    End Function

    Private Function HandleExamine() As IUIState
        Dim location = World.Avatar.Map.GetLocation(World.GetStatistic(StatisticType.ViewColumn), World.GetStatistic(StatisticType.ViewRow))
        If Not location.Visible Then
            Return Nothing
        End If
        Return New DialogState(Buffer, World, Settings, New ExamineLocationDialog(location))
    End Function

    Private Function HandleMove(deltaX As Integer, deltaY As Integer) As IUIState
        World.ChangeStatistic(StatisticType.ViewColumn, deltaX)
        World.ChangeStatistic(StatisticType.ViewRow, deltaY)
        Return Me
    End Function
End Class
