Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module LocationExtensions
    <Extension>
    Friend Function NextLocation(location As ILocation, direction As String) As ILocation
        Dim descriptor = DirectionTypes.Descriptors(direction)
        Dim nextColumn = descriptor.GetNextColumn(location.Column)
        Dim nextRow = descriptor.GetNextRow(location.Row)
        Return location.Map.GetLocation(nextColumn, nextRow)
    End Function
    <Extension>
    Friend Function HandleBump(location As ILocation, character As ICharacter) As IDialog
        Return location.Descriptor.OnBump(location, character)
    End Function
    <Extension>
    Friend Sub HandleLeave(location As ILocation, character As ICharacter)
        location.Descriptor.OnLeave(location, character)
    End Sub
    <Extension>
    Friend Function HandleEnter(location As ILocation, character As ICharacter) As IDialog
        Return location.Descriptor.OnEnter(location, character)
    End Function
    <Extension>
    Friend Function GetForageGenerator(location As ILocation) As IGenerator
        If location.HasStatistic(StatisticType.ForageGeneratorId) Then
            Return location.World.GetGenerator(location.GetStatistic(StatisticType.ForageGeneratorId))
        End If
        Dim forageTableType = location.GetMetadata(MetadataType.ForageTable)
        If Not String.IsNullOrEmpty(forageTableType) Then
            Dim generator As IGenerator = location.World.CreateGenerator(forageTableType)
            location.SetStatistic(StatisticType.ForageGeneratorId, generator.GeneratorId)
            Return generator
        End If
        Return Nothing
    End Function
    <Extension>
    Sub SetDismantleTable(location As ILocation, generator As IGenerator)
        location.SetStatistic(StatisticType.DismantleGeneratorId, generator.GeneratorId)
    End Sub
    <Extension>
    Function GetDismantleTable(location As ILocation) As IGenerator
        Return location.World.GetGenerator(location.GetStatistic(StatisticType.DismantleGeneratorId))
    End Function
End Module
