Imports TGGD.Business

Public Class FishingNetItemTypeDescriptor
    Inherits MendableItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            NameOf(FishingNetItemTypeDescriptor),
            "Net",
            0,
            False,
            MAXIMUM_DURABILITY,
            {TagType.CanFish},
            NameOf(TwineItemTypeDescriptor),
            MAXIMUM_DURABILITY \ 2,
                   New Dictionary(Of String, Integer))
    End Sub

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a fishing net."),
            New DialogLine(MoodType.Info, "For fishing, not wearing."),
            New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
