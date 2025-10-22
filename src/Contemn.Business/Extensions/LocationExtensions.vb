Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module LocationExtensions
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
End Module
