Friend Class WaterStatisticTypeDescriptor
    Inherits StatisticTypeDescriptor

    Public Sub New()
        MyBase.New(Business.StatisticType.Water, "WATER")
    End Sub

    Friend Overrides Function Format(statisticValue As Integer, statisticMinimum As Integer, statisticMaximum As Integer) As String
        Return $"{StatisticTypeName} {100 * statisticValue \ statisticMaximum}%"
    End Function
End Class
