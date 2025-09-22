Imports TGGD.Business

Friend Class AxeItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            Business.ItemType.Axe,
            "Axe",
            0,
            False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
        item.SetStatisticRange(
            StatisticType.Durability,
            MAXIMUM_DURABILITY,
            0,
            MAXIMUM_DURABILITY)
        item.SetTag(TagType.CanChop, True)
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

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Return Array.Empty(Of IDialogChoice)
    End Function

    Friend Overrides Function Describe(item As Item) As IEnumerable(Of IDialogLine)
        Return {
                New DialogLine(MoodType.Info, "It's an axe."),
                New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
