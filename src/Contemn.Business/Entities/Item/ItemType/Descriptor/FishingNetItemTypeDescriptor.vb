Imports TGGD.Business

Friend Class FishingNetItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_DURABILITY = 20
    Private ReadOnly MendChoice As (Choice As String, Text As String) = ("MEND_CHOICE", "Mend")

    Public Sub New()
        MyBase.New(
            Business.ItemType.FishingNet,
            "Net",
            0,
            False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
        item.SetStatisticRange(StatisticType.Durability, MAXIMUM_DURABILITY, 0, MAXIMUM_DURABILITY)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return $"{ItemTypeName}({item.GetStatistic(StatisticType.Durability)}/{item.GetStatisticMaximum(StatisticType.Durability)})"
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case MendChoice.Choice
                Return Mend(item, character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Mend(item As IItem, character As ICharacter) As IDialog
        Return New MessageDialog(
            GenerateMendLines(item, character),
            {(OK_CHOICE, OK_TEXT, Function() New ItemOfTypeDialog(character, item), True)},
            Function() New ItemOfTypeDialog(character, item))
    End Function

    Private Function GenerateMendLines(item As IItem, character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        Dim result = character.ProcessTurn().ToList
        Dim originalDurability = item.GetStatistic(StatisticType.Durability)
        item.ChangeStatistic(StatisticType.Durability, item.GetStatisticMaximum(StatisticType.Durability) \ 2)
        Dim mendDurability = item.GetStatistic(StatisticType.Durability) - originalDurability
        result.Add((MoodType.Info, $"+{mendDurability} {StatisticType.Durability.ToStatisticTypeDescriptor.StatisticTypeName}({item.GetStatistic(StatisticType.Durability)}/{item.GetStatisticMaximum(StatisticType.Durability)})"))
        Dim twine = character.GetItemOfType(Business.ItemType.Twine)
        character.RemoveItem(twine)
        result.Add((MoodType.Info, $"-1 {twine.Name}({character.GetCountOfItemType(twine.ItemType)})"))
        twine.Recycle()
        Return result
    End Function

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim choices As New List(Of (Choice As String, Text As String))
        If CanMend(item, character) Then
            choices.Add(MendChoice)
        End If
        Return choices
    End Function

    Private Function CanMend(item As Item, character As ICharacter) As Boolean
        Return Not item.IsStatisticAtMaximum(StatisticType.Durability) AndAlso character.HasItemsOfType(Business.ItemType.Twine)
    End Function

    Friend Overrides Function Describe(item As Item) As IEnumerable(Of (Mood As String, Text As String))
        Return {
            (MoodType.Info, "Its a fishing net."),
            (MoodType.Info, item.FormatStatistic(StatisticType.Durability))
        }
    End Function
End Class
