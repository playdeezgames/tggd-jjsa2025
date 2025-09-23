Imports TGGD.Business

Friend Class FishingNetItemTypeDescriptor
    Inherits MendableItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            Business.ItemType.FishingNet,
            "Net",
            0,
            False,
            MAXIMUM_DURABILITY,
            {TagType.CanFish},
            Business.ItemType.Twine,
            MAXIMUM_DURABILITY \ 2)
    End Sub

    Friend Overrides Function Describe(item As Item) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a fishing net."),
            New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
