Imports TGGD.Business

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private Const CATCH_ANOTHER_TEXT = "Catch Another"

    Public Sub New()
        MyBase.New(
            Business.VerbType.Fish,
            Business.VerbCategoryType.Bump,
            "Fish")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        Dim success = RNG.GenerateBoolean(bumpLocation.GetStatistic(StatisticType.Depletion), bumpLocation.GetStatistic(StatisticType.Resource))
        If success Then
            Return CatchFish(character, bumpLocation)
        End If
        Return NoCatch(character)
    End Function

    Private Function NoCatch(character As ICharacter) As IDialog
        character.PlaySfx(Sfx.Shucks)
        Return New MessageDialog(
            character.ProcessTurn().
                Append(New DialogLine(MoodType.Info, $"You catch nothing.")),
            {
                (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                (TRY_AGAIN_CHOICE, TRY_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
            },
            Function() Nothing)
    End Function

    Private Function CatchFish(character As ICharacter, bumpLocation As ILocation) As IDialog
        Dim item = character.World.CreateItem(NameOf(FishItemTypeDescriptor), character)
        bumpLocation.ChangeStatistic(StatisticType.Depletion, 1)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -1)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        Return New MessageDialog(
                    character.ProcessTurn().
                        Append(New DialogLine(MoodType.Info, $"+1 {item.Name}({character.GetCountOfItemType(NameOf(FishItemTypeDescriptor))})")).
                        Concat(character.Items.First(Function(x) x.GetTag(TagType.CanFish)).Deplete(character)),
                    {
                        (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                        (TRY_AGAIN_CHOICE, CATCH_ANOTHER_TEXT, Function() Perform(character), CanPerform(character))
                    },
                    Function() Nothing)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        If Not MyBase.CanPerform(character) Then
            Return False
        End If
        If Not character.GetBumpLocation().GetTag(TagType.CanFish) Then
            Return False
        End If
        If Not character.Items.Any(Function(x) x.GetTag(TagType.CanFish)) Then
            Return False
        End If
        Return True
    End Function
End Class
