Imports System.Runtime.CompilerServices

Friend Module StatisticTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, StatisticTypeDescriptor) =
        New List(Of StatisticTypeDescriptor) From
        {
            New HealthStatisticTypeDescriptor(),
            New SatietyStatisticTypeDescriptor(),
            New HydrationStatisticTypeDescriptor(),
            New IllnessStatisticTypeDescriptor(),
            New ScoreStatisticTypeDescriptor(),
            New DurabilityStatisticTypeDescriptor(),
            New FuelStatisticTypeDescriptor(),
            New RecoveryStatisticTypeDescriptor(),
            New WaterStatisticTypeDescriptor()
        }.ToDictionary(Function(x) x.StatisticType, Function(x) x)
End Module
