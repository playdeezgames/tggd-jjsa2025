Imports TGGD.Business

Public Class TorchItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_FUEL = 5
    Shared ReadOnly EXTINGUISH_CHOICE As String = NameOf(EXTINGUISH_CHOICE)
    Const EXTINGUISH_TEXT = "Extinguish"
    Public Sub New()
        MyBase.New(
            NameOf(TorchItemTypeDescriptor),
            "Torch",
            0,
            False,
            {
                TagType.IsIgnitable,
                TagType.CanRefuel,
                TagType.CanLight
            },
                   New Dictionary(Of String, Integer))
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
        MyBase.HandleInitialize(item)
        item.World.ActivateItem(item)
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
        Select Case choice
            Case EXTINGUISH_CHOICE
                Return Extinguish(item, character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Extinguish(item As IItem, character As ICharacter) As IDialog
        item.SetTag(TagType.IsLit, False)
        Return New OkDialog(
            "Sizzle!",
            {
                New DialogLine(MoodType.Info, $"You extinguish {item.Name}.")
            },
            Function() New ItemOfTypeDialog(character, item))
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice)
        If item.GetTag(TagType.IsLit) Then
            result.Add(New DialogChoice(EXTINGUISH_CHOICE, EXTINGUISH_TEXT))
        End If
        Return result
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a torch.")
            }
    End Function

    Friend Overrides Function OnProcessTurn(item As IItem) As IEnumerable(Of IDialogLine)
        Dim result = MyBase.OnProcessTurn(item).ToList
        If item.GetTag(TagType.IsLit) Then
            item.ChangeStatistic(StatisticType.Fuel, -1)
            If item.IsStatisticAtMinimum(StatisticType.Fuel) Then
                item.SetTag(TagType.IsLit, False)
                result.Add(New DialogLine(MoodType.Info, $"{item.Name} has gone out."))
            End If
        End If
        Return result
    End Function
End Class
