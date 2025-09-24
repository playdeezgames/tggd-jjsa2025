Imports TGGD.Business

Friend Class KnifeItemTypeDescriptor
    Inherits MendableItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            Business.ItemType.Knife,
            "Knife",
            0,
            False,
            MAXIMUM_DURABILITY,
            {TagType.CanCut},
            Business.ItemType.Blade,
            MAXIMUM_DURABILITY)
    End Sub

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
                New DialogLine(MoodType.Info, "It's a knife."),
                New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
