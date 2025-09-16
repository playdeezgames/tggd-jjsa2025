Friend Class HydrationStatisticTypeDescriptor
    Inherits StatisticTypeDescriptor

    Public Sub New()
        MyBase.New(Business.StatisticType.Hydration, "Hydration")
    End Sub

    Friend Overrides Function Format(statisticValue As Integer, statisticMinimum As Integer, statisticMaximum As Integer) As String
        Return $"HYD: {statisticValue}/{statisticMaximum}"
    End Function
End Class
