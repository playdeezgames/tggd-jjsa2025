Imports TGGD.Business

Public Class AxeItemTypeDescriptor
    Inherits MendableItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            NameOf(AxeItemTypeDescriptor),
            "Axe",
            0,
            False,
            MAXIMUM_DURABILITY,
            {TagType.CanChop, TagType.CanSharpen},
            NameOf(SharpRockItemTypeDescriptor),
            MAXIMUM_DURABILITY)
    End Sub

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
                New DialogLine(MoodType.Info, "It's an axe."),
                New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
