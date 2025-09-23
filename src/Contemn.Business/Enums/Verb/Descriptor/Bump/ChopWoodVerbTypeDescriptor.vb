Imports TGGD.Business

Friend Class ChopWoodVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Const RESOURCE_PER_LOG = 5
    Const CHOP_AGAIN_TEXT = "Chop Again"

    Public Sub New()
        MyBase.New(
            Business.VerbType.ChopWood,
            Business.VerbCategoryType.Bump,
            "Chop Wood")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        Dim item = character.World.CreateItem(ItemType.Log, character)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -RESOURCE_PER_LOG)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        Return New MessageDialog(
                    character.ProcessTurn().
                        Append(New DialogLine(MoodType.Info, $"+1 {item.Name}({character.GetCountOfItemType(ItemType.Log)})")).
                        Concat(DepleteAxe(character)),
                    {
                        (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                        (TRY_AGAIN_CHOICE, CHOP_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
                    },
                    Function() Nothing)
    End Function

    Private Function DepleteAxe(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim lines As New List(Of IDialogLine)
        Dim chopper = character.Items.First(Function(x) x.GetTag(TagType.CanChop))
        chopper.ChangeStatistic(StatisticType.Durability, -1)
        Dim netRuined = chopper.IsStatisticAtMinimum(StatisticType.Durability)
        If netRuined Then
            lines.Add(
            New DialogLine(
                MoodType.Info,
                $"Yer {chopper.Name} is ruined!"))
            character.RemoveAndRecycleItem(chopper)
        Else
            lines.Add(
                New DialogLine(
                    MoodType.Info,
                    $"-1 {StatisticType.Durability.ToStatisticTypeDescriptor.StatisticTypeName} {chopper.Name}"))
            If chopper.GetStatistic(StatisticType.Durability) < chopper.GetStatisticMaximum(StatisticType.Durability) \ 3 Then
                lines.Add(
                New DialogLine(
                    MoodType.Warning,
                    $"Repair yer {chopper.Name} soon."))
            End If
        End If
        Return lines
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation.GetTag(TagType.IsChoppable) AndAlso
            bumpLocation.GetStatistic(StatisticType.Resource) >= RESOURCE_PER_LOG AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanChop))
    End Function
End Class
