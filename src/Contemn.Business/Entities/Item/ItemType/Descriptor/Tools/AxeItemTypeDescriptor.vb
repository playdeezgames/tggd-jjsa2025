Imports TGGD.Business

Friend Class AxeItemTypeDescriptor
    Inherits MendableItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            Business.ItemType.Axe,
            "Axe",
            0,
            False,
            MAXIMUM_DURABILITY,
            {TagType.CanChop},
            Business.ItemType.SharpRock,
            MAXIMUM_DURABILITY)
    End Sub

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
                New DialogLine(MoodType.Info, "It's an axe."),
                New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
