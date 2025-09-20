Friend Class DurabilityStatisticTypeDescriptor
    Inherits StatisticTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.StatisticType.Durability,
            "Durability")
    End Sub

    Friend Overrides Function Format(statisticValue As Integer, statisticMinimum As Integer, statisticMaximum As Integer) As String
        Return $"DUR: {statisticValue}/{statisticMaximum}"
    End Function
End Class
