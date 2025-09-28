Imports TGGD.Business

Friend Class FiredPotItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Const MAXIMUM_WATER = 10
    Shared ReadOnly DRINK_CHOICE As String = NameOf(DRINK_CHOICE)
    Const DRINK_TEXT = "Drink"
    Public Sub New()
        MyBase.New(
            NameOf(FiredPotItemTypeDescriptor),
            "Clay Pot",
            0,
            False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
        item.SetStatisticRange(StatisticType.Water, 0, 0, MAXIMUM_WATER)
        item.SetTag(TagType.IsFillable, True)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As IItem) As String
        Dim safetyText = If(
            item.IsStatisticAtMinimum(StatisticType.Water),
            "",
            If(item.GetTag(TagType.Safe), " Safe", " Unsafe"))
        Return $"{ItemTypeName}({100 * item.GetStatistic(StatisticType.Water) \ item.GetStatisticMaximum(StatisticType.Water)}%{safetyText})"
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case DRINK_CHOICE
                Return DoDrink(item, character)
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Private Function DoDrink(item As IItem, character As ICharacter) As IDialog
        Dim messageLines = character.World.ProcessTurn()
        Dim hydrationDelta = character.GetStatisticMaximum(StatisticType.Hydration) - character.GetStatistic(StatisticType.Hydration)
        character.ChangeStatistic(StatisticType.Hydration, hydrationDelta)
        item.ChangeStatistic(StatisticType.Water, -1)
        Dim illness = 0
        If RNG.GenerateBoolean(If(item.GetTag(TagType.Safe), 0, 1), 1) Then
            illness = RNG.RollXDY(1, 4)
            character.ChangeStatistic(StatisticType.Illness, illness)
        End If
        Return New OkDialog(
            messageLines.
            Append(New DialogLine(MoodType.Info, $"+{hydrationDelta} {character.FormatStatistic(StatisticType.Hydration)}")).
            Append(New DialogLine(MoodType.Info, $"-1 {item.FormatStatistic(StatisticType.Water)}")).
            AppendIf(illness > 0, New DialogLine(MoodType.Info, $"+{illness} {StatisticType.Illness.ToStatisticTypeDescriptor.StatisticTypeName}")),
            Function() New ItemOfTypeDialog(character, item))
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice)
        If Not item.IsStatisticAtMinimum(StatisticType.Water) Then
            result.Add(New DialogChoice(DRINK_CHOICE, DRINK_TEXT))
        End If
        Return result
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a clay pot."),
            New DialogLine(MoodType.Info, item.FormatStatistic(StatisticType.Water))
            }.
            AppendIf(
                Not item.IsStatisticAtMinimum(StatisticType.Water),
                New DialogLine(MoodType.Info, If(item.GetTag(TagType.Safe), "Safe", "Unsafe")))
    End Function
End Class
