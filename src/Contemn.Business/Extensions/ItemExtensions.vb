Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module ItemExtensions
    <Extension>
    Friend Function Deplete(tool As IItem, character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim lines As New List(Of IDialogLine)
        If Not tool.HasStatistic(StatisticType.Durability) Then
            Return lines
        End If
        tool.ChangeStatistic(StatisticType.Durability, -1)
        Dim itemRuined = tool.IsStatisticAtMinimum(StatisticType.Durability)
        If itemRuined Then
            lines.Add(
            New DialogLine(
                MoodType.Info,
                $"Yer {tool.Name} is ruined!"))
            For Each entry In tool.Descriptor.DepletionTable
                Dim entryDescriptor = ItemTypes.Descriptors(entry.Key)
                lines.Add(New DialogLine(MoodType.Info, $"+{entry.Value} {entryDescriptor.ItemTypeName}"))
                For Each dummy In Enumerable.Range(0, entry.Value)
                    tool.World.CreateItem(entry.Key, character)
                Next
            Next
            character.RemoveAndRecycleItem(tool)
        Else
            lines.Add(
                New DialogLine(
                    MoodType.Info,
                    $"-1 {StatisticTypes.Descriptors(StatisticType.Durability).StatisticTypeName} {tool.Name}"))
            If tool.GetStatistic(StatisticType.Durability) < tool.GetStatisticMaximum(StatisticType.Durability) \ 3 Then
                lines.Add(
                New DialogLine(
                    MoodType.Warning,
                    $"Repair yer {tool.Name} soon."))
            End If
        End If
        Return lines
    End Function
End Module
