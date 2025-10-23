Imports Contemn.Business
Imports TGGD.UI

Friend Class NavigationState
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
        RenderStatistics()
        RenderMessages()
    End Sub

    Private Sub RenderStatistics()
        Dim avatar = World.Avatar
        Dim x = VIEW_WIDTH
        Dim y = 0
        Buffer.Write(x, y, avatar.FormatStatistic(StatisticType.Score), Hue.Green, Hue.Black, False)
        y += 2
        Buffer.Write(x, y, avatar.FormatStatistic(StatisticType.Health), Hue.Red, Hue.Black, False)
        y += 1
        Buffer.Write(x, y, avatar.FormatStatistic(StatisticType.Satiety), Hue.Magenta, Hue.Black, False)
        y += 1
        Buffer.Write(x, y, avatar.FormatStatistic(StatisticType.Hydration), Hue.Blue, Hue.Black, False)
        y += 1
        y = RenderIllness(x, y, avatar)
        Buffer.Write(x, y, $"Terrain: {avatar.Location.Name}", Hue.LightGray, Hue.Black, False)
        y += 1
        y = RenderToolTip(x, y, avatar)
        y = RenderGroundItems(x, y, avatar)
    End Sub

    Private Function RenderToolTip(x As Integer, y As Integer, character As ICharacter) As Integer
        If character.Location.HasMetadata(MetadataType.ForageTable) Then
            Buffer.Write(x, y, $"(can forage)", Hue.DarkGray, Hue.Black, False)
            y += 1
        End If
        Return y
    End Function

    Private Function RenderGroundItems(x As Integer, y As Integer, character As ICharacter) As Integer
        If character.Location.HasItems Then
            Buffer.Write(x, y, $"Ground Items", Hue.Black, Hue.Yellow, False)
            y += 1
        End If
        Return y
    End Function

    Private Function RenderIllness(x As Integer, y As Integer, character As ICharacter) As Integer
        If character.GetStatistic(StatisticType.Illness) > character.GetStatisticMinimum(StatisticType.Illness) Then
            Buffer.Write(x, y, character.FormatStatistic(StatisticType.Illness), Hue.Brown, Hue.Black, False)
            Return y + 1
        End If
        Return y
    End Function

    Private Sub RenderMessages()
        While World.MessageCount > MESSAGE_LINES
            World.DismissMessage()
        End While
        Dim row = VIEW_HEIGHT
        For Each line In Enumerable.Range(0, World.MessageCount)
            Dim message = World.GetMessage(line)
            Dim colors = moodColors(message.Mood)
            Buffer.Write(0, row, message.Text, colors.ForegroundColor, colors.BackgroundColor, False)
            row += 1
        Next
    End Sub

    Private Sub RenderMap()
        Dim map = World.Avatar.Map
        For Each columnOffset In Enumerable.Range(-VIEW_WIDTH \ 2, VIEW_WIDTH)
            Dim column = World.Avatar.Column + columnOffset
            Dim displayColumn = VIEW_WIDTH \ 2 + columnOffset
            For Each rowOffset In Enumerable.Range(-VIEW_HEIGHT \ 2, VIEW_HEIGHT)
                Dim row = World.Avatar.Row + rowOffset
                Dim displayRow = VIEW_HEIGHT \ 2 + rowOffset
                Dim location = map.GetLocation(column, row)
                RenderLocation(displayColumn, displayRow, location)
            Next
        Next
    End Sub

    Private Sub RenderLocation(displayColumn As Integer, displayRow As Integer, location As ILocation)
        If location Is Nothing OrElse Not location.GetTag(TagType.Visible) Then
            Buffer.Fill(displayColumn, displayRow, 1, 1, &HB0, Hue.Cyan, Hue.Black, False)
        ElseIf location.HasCharacter Then
            Buffer.SetPixel(displayColumn, displayRow, location.Character.ToPixel(False))
        ElseIf location.HasItems Then
            Buffer.SetPixel(displayColumn, displayRow, location.Items.First().ToPixel(False))
        Else
            Buffer.SetPixel(displayColumn, displayRow, location.ToPixel(False))
        End If
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Select Case command
            Case UI.Command.Red
                Return New GameMenuState(Buffer, World, Settings)
            Case UI.Command.Up
                Return HandleMove(NameOf(MoveNorthVerbTypeDescriptor))
            Case UI.Command.Down
                Return HandleMove(NameOf(MoveSouthVerbTypeDescriptor))
            Case UI.Command.Left
                Return HandleMove(NameOf(MoveWestVerbTypeDescriptor))
            Case UI.Command.Right
                Return HandleMove(NameOf(MoveEastVerbTypeDescriptor))
            Case UI.Command.Green
                Return New DialogState(Buffer, World, Settings, CharacterActionsDialog.LaunchMenu(World.Avatar).Invoke())
            Case Else
                Return Me
        End Select
    End Function

    Private Function HandleMove(verbType As String) As IUIState
        Dim dialog = World.Avatar.Perform(verbType)
        If dialog IsNot Nothing Then
            Return New DialogState(Buffer, World, Settings, dialog)
        End If
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function
End Class
