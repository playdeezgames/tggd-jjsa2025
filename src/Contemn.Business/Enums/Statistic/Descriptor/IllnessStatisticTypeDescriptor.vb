Friend Class IllnessStatisticTypeDescriptor
    Inherits StatisticTypeDescriptor

    Public Sub New()
        MyBase.New(Business.StatisticType.Illness, "Illness")
    End Sub

    Friend Overrides Function Format(statisticValue As Integer, statisticMinimum As Integer, statisticMaximum As Integer) As String
        Return $"{StatisticTypeName}: {statisticValue}"
    End Function
End Class
