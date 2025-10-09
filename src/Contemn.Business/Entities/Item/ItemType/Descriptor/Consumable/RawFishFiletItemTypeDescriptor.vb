Imports TGGD.Business

Public Class RawFishFiletItemTypeDescriptor
    Inherits ConsumableItemTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(RawFishFiletItemTypeDescriptor),
            "Raw Filet",
            0,
            True,
            (1, 1, 4))
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
        item.SetStatistic(StatisticType.Satiety, 15)
        item.SetTag(TagType.IsCookable, True)
        item.SetMetadata(MetadataType.CookedItemType, NameOf(CookedFishFiletItemTypeDescriptor))
    End Sub

    Protected Overrides Function OtherChoice(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As IItem) As String
        Return ItemTypeName
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a raw fish filet.")
        }
    End Function
End Class
