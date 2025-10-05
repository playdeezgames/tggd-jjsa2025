Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module ItemExtensions
    <Extension>
    Friend Sub Place(item As IItem, location As ILocation)
        location.LocationType = item.GetMetadata(MetadataType.PlaceLocationType)
    End Sub
    <Extension>
    Friend Function Deplete(tool As IItem, character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim lines As New List(Of IDialogLine)
        tool.ChangeStatistic(StatisticType.Durability, -1)
        Dim netRuined = tool.IsStatisticAtMinimum(StatisticType.Durability)
        If netRuined Then
            lines.Add(
            New DialogLine(
                MoodType.Info,
                $"Yer {tool.Name} is ruined!"))
            character.RemoveAndRecycleItem(tool)
        Else
            lines.Add(
                New DialogLine(
                    MoodType.Info,
                    $"-1 {StatisticType.Durability.ToStatisticTypeDescriptor.StatisticTypeName} {tool.Name}"))
            If tool.GetStatistic(StatisticType.Durability) < tool.GetStatisticMaximum(StatisticType.Durability) \ 3 Then
                lines.Add(
                New DialogLine(
                    MoodType.Warning,
                    $"Repair yer {tool.Name} soon."))
            End If
        End If
        Return lines
    End Function
    <Extension>
    Sub SetDismantleTable(item As IItem, generator As IGenerator)
        item.SetStatistic(StatisticType.DismantleGeneratorId, generator.GeneratorId)
    End Sub
    <Extension>
    Function GetDismantleTable(item As IItem) As IGenerator
        Return item.World.GetGenerator(item.GetStatistic(StatisticType.DismantleGeneratorId))
    End Function
End Module
