Imports TGGD.Business

Friend Class TorchItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_FUEL = 5
    Public Sub New()
        MyBase.New(
            NameOf(TorchItemTypeDescriptor),
            "Torch",
            0,
            False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
        item.World.ActivateItem(item)
        item.SetTag(TagType.IsIgnitable, True)
        item.SetTag(TagType.CanRefuel, True)
        item.SetTag(TagType.CanLight, True)
        item.SetStatisticRange(
            StatisticType.Fuel,
            MAXIMUM_FUEL,
            0,
            MAXIMUM_FUEL)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As IItem) As String
        Return $"{ItemTypeName}({item.GetStatistic(StatisticType.Fuel)}/{item.GetStatisticMaximum(StatisticType.Fuel)} {If(item.GetTag(TagType.IsLit), "lit", "unlit")})"
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice)
        Return result
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a torch.")
            }
    End Function

    Friend Overrides Sub OnProcessTurn(item As Item)
        MyBase.OnProcessTurn(item)
        If item.GetTag(TagType.IsLit) Then
            item.ChangeStatistic(StatisticType.Fuel, -1)
            If item.IsStatisticAtMinimum(StatisticType.Fuel) Then
                item.SetTag(TagType.IsLit, False)
            End If
        End If
    End Sub
End Class
