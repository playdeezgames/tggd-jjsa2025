Imports TGGD.Business

Public MustInherit Class ConsumableItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Private Shared ReadOnly EAT_ANOTHER_CHOICE As String = NameOf(EAT_ANOTHER_CHOICE)
    Private Const EAT_ANOTHER_TEXT = "Eat Another..."
    Private ReadOnly foodPoisoningStats As (Hazard As Integer, Safety As Integer, Severity As Integer)

    Protected Sub New(
                     itemType As String,
                     itemTypeName As String,
                     itemCount As Integer,
                     isAggregate As Boolean,
                     foodPoisoningStats As (Hazard As Integer, Safety As Integer, Severity As Integer),
                     autoTags As IEnumerable(Of String),
                     depletionTable As IReadOnlyDictionary(Of String, Integer))
        MyBase.New(itemType,
                   itemTypeName,
                   itemCount,
                   isAggregate,
                   autoTags,
                   depletionTable)
        Me.foodPoisoningStats = foodPoisoningStats
    End Sub

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case EAT_CHOICE
                Return Eat(item, character)
            Case Else
                Return OtherChoice(item, character, choice)
        End Select
    End Function

    Protected MustOverride Function OtherChoice(item As IItem, character As ICharacter, choice As String) As IDialog

    Private Function Eat(item As IItem, character As ICharacter) As IDialog
        Dim lines As New List(Of IDialogLine)
        character.ChangeStatistic(StatisticType.Satiety, item.GetStatistic(StatisticType.Satiety))
        lines.Add(New DialogLine(MoodType.Info, $"+{item.GetStatistic(StatisticType.Satiety)} {StatisticType.Satiety.ToStatisticTypeDescriptor.StatisticTypeName}({character.GetStatistic(StatisticType.Satiety)})"))
        If RNG.GenerateBoolean(foodPoisoningStats.Safety, foodPoisoningStats.Hazard, Nothing) Then
            Dim illness = RNG.RollXDY(1, foodPoisoningStats.Severity)
            character.ChangeStatistic(StatisticType.Illness, illness)
            lines.Add(New DialogLine(MoodType.Info, "You get food poisoning."))
            lines.Add(New DialogLine(MoodType.Info, $"+{illness} {StatisticType.Illness.ToStatisticTypeDescriptor.StatisticTypeName}({character.GetStatistic(StatisticType.Illness)})"))
        End If
        character.RemoveItem(item)
        lines.Add(New DialogLine(MoodType.Info, $"-1 {item.Name}({character.GetCountOfItemType(ItemType)})"))
        Dim caption = $"Ate {item.Name}"
        item.Recycle()
        character.Platform.PlaySfx(Sfx.Eat)
        Return New MessageDialog(
            caption,
            lines,
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT, ItemTypeDialog.LaunchMenu(character, ItemType), True),
                (EAT_ANOTHER_CHOICE, EAT_ANOTHER_TEXT, Function() Eat(character.GetItemOfType(ItemType), character), character.HasItemsOfType(ItemType))
            },
            ItemTypeDialog.LaunchMenu(character, ItemType))
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Return {New DialogChoice(EAT_CHOICE, EAT_TEXT)}
    End Function
End Class
