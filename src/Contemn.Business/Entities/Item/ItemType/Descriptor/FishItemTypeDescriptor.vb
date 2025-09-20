Imports TGGD.Business

Friend Class FishItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Private Shared ReadOnly EAT_ANOTHER_CHOICE As String = NameOf(EAT_ANOTHER_CHOICE)
    Private Const EAT_ANOTHER_TEXT = "Eat Another..."

    Public Sub New()
        MyBase.New(
            Business.ItemType.Fish,
            "Fish",
            0,
            True)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
        item.SetStatistic(StatisticType.Satiety, 25)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return ItemTypeName
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case EAT_CHOICE
                Return Eat(item, character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Eat(item As IItem, character As ICharacter) As IDialog
        Dim lines As New List(Of (Mood As String, Text As String))
        character.ChangeStatistic(StatisticType.Satiety, item.GetStatistic(StatisticType.Satiety))
        lines.Add((MoodType.Info, $"+{item.GetStatistic(StatisticType.Satiety)} {StatisticType.Satiety.ToStatisticTypeDescriptor.StatisticTypeName}({character.GetStatistic(StatisticType.Satiety)})"))
        If RNG.GenerateBoolean(1, 1) Then
            Dim illness = RNG.RollXDY(1, 4)
            character.ChangeStatistic(StatisticType.Illness, illness)
            lines.Add((MoodType.Info, "You get food poisoning."))
            lines.Add((MoodType.Info, $"+{illness} {StatisticType.Illness.ToStatisticTypeDescriptor.StatisticTypeName}({character.GetStatistic(StatisticType.Illness)})"))
        End If
        character.RemoveItem(item)
        lines.Add((MoodType.Info, $"-1 {item.Name}({character.GetCountOfItemType(ItemType)})"))
        item.Recycle()
        character.PlaySfx(Sfx.Eat)
        Return New MessageDialog(
            lines,
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT, ItemTypeDialog.LaunchMenu(character, ItemType), True),
                (EAT_ANOTHER_CHOICE, EAT_ANOTHER_TEXT, Function() Eat(character.GetItemOfType(ItemType), character), character.HasItemsOfType(ItemType))
            },
            ItemTypeDialog.LaunchMenu(character, ItemType))
    End Function

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Return {(EAT_CHOICE, EAT_TEXT)}
    End Function

    Friend Overrides Function Describe(item As Item) As IEnumerable(Of (Mood As String, Text As String))
        Return {
            (MoodType.Info, "Its a fish.")
        }
    End Function
End Class
