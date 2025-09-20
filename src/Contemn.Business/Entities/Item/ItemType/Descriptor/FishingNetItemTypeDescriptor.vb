Imports TGGD.Business

Friend Class FishingNetItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20
    Public Sub New()
        MyBase.New(
            Business.ItemType.FishingNet,
            "Net",
            0,
            False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
        item.SetStatisticRange(StatisticType.Durability, MAXIMUM_DURABILITY, 0, MAXIMUM_DURABILITY)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return $"{ItemTypeName}({item.GetStatistic(StatisticType.Durability)}/{item.GetStatisticMaximum(StatisticType.Durability)})"
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Return Array.Empty(Of (Choice As String, Text As String))
    End Function

    Friend Overrides Function Describe(item As Item) As IEnumerable(Of (Mood As String, Text As String))
        Return {
            (MoodType.Info, "Its a fishing net."),
            (MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
