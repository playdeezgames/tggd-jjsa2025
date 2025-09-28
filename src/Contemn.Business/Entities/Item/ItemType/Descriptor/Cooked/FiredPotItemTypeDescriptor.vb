Imports TGGD.Business

Friend Class FiredPotItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_WATER = 10
    Public Sub New()
        MyBase.New(
            NameOf(FiredPotItemTypeDescriptor),
            "Clay Pot",
            0,
            False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
        item.SetStatisticRange(StatisticType.Water, 0, 0, MAXIMUM_WATER)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As IItem) As String
        Return ItemTypeName
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Return Array.Empty(Of IDialogChoice)
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a clay pot."),
            New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Water))
            }.
            AppendIf(
                Not item.IsStatisticAtMinimum(StatisticType.Water),
                New DialogLine(MoodType.Info, If(item.GetTag(TagType.Safe), "Safe", "Unsafe")))
    End Function
End Class
