Imports TGGD.Business

Public Class HammerItemTypeDescriptor
    Inherits MendableItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20

    Public Sub New()
        MyBase.New(
            NameOf(HammerItemTypeDescriptor),
            "Hammer",
            0,
            False,
            MAXIMUM_DURABILITY,
            {TagType.CanHammer},
            NameOf(RockItemTypeDescriptor),
            MAXIMUM_DURABILITY)
    End Sub

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
                New DialogLine(MoodType.Info, "It's a hammer."),
                New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
