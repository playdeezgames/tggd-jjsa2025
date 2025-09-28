Imports TGGD.Business

Friend Class WaterLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Water, "Water")
    End Sub

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnInitialize(location As Location)
        location.SetStatistic(StatisticType.Resource, 20)
        location.SetStatisticMinimum(StatisticType.Resource, 0)
        location.SetStatistic(StatisticType.Depletion, 0)
        location.SetTag(TagType.IsFishable, True)
        location.SetTag(TagType.IsDiggable, True)
        location.SetTag(TagType.CanFill, True)
    End Sub

    Friend Overrides Sub OnProcessTurn(location As Location)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return New BumpDialog(character)
    End Function

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return False
    End Function
End Class
