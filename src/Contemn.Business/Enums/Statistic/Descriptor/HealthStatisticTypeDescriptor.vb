Friend Class HealthStatisticTypeDescriptor
    Inherits StatisticTypeDescriptor

    Public Sub New()
        MyBase.New(Business.StatisticType.Health, "Health")
    End Sub

    Friend Overrides Function Format(statisticValue As Integer, statisticMinimum As Integer, statisticMaximum As Integer) As String
        Return $"HLT: {statisticValue}/{statisticMaximum}"
    End Function
End Class
