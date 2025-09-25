Imports TGGD.Business

Friend Class DigClayVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.VerbType.DigClay,
            Business.VerbCategoryType.Bump,
            "Dig Clay")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        Dim success = RNG.GenerateBoolean(bumpLocation.GetStatistic(StatisticType.Depletion), bumpLocation.GetStatistic(StatisticType.Resource))
        If success Then
            Return FindClay(character, bumpLocation)
        End If
        Return FindNothing(character)
    End Function

    Private Function FindClay(character As ICharacter, bumpLocation As ILocation) As IDialog
        Dim item = character.World.CreateItem(NameOf(ClayItemTypeDescriptor), character)
        bumpLocation.ChangeStatistic(StatisticType.Depletion, 1)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -1)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        Return New MessageDialog(
                    character.ProcessTurn().
                        Append(New DialogLine(MoodType.Info, $"+1 {item.Name}({character.GetCountOfItemType(NameOf(FishItemTypeDescriptor))})")).
                        Concat(character.Items.First(Function(x) x.GetTag(TagType.CanDig)).Deplete(character)),
                    {
                        (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                        (TRY_AGAIN_CHOICE, TRY_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
                    },
                    Function() Nothing)
    End Function

    Private Function FindNothing(character As ICharacter) As IDialog
        character.PlaySfx(Sfx.Shucks)
        Return New MessageDialog(
            character.ProcessTurn().
                Append(New DialogLine(MoodType.Info, $"You mind nothing.")),
            {
                (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                (TRY_AGAIN_CHOICE, TRY_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
            },
            Function() Nothing)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsDiggable) AndAlso
            bumpLocation.LocationType = LocationType.Water AndAlso
            Not bumpLocation.IsStatisticAtMinimum(StatisticType.Resource) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanDig))
    End Function
End Class
