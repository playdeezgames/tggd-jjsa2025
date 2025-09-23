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
        Dim item = character.World.CreateItem(ItemType.Fish, character)
        bumpLocation.ChangeStatistic(StatisticType.Depletion, 1)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -1)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        Return New MessageDialog(
                    character.ProcessTurn().
                        Append(New DialogLine(MoodType.Info, $"+1 {item.Name}({character.GetCountOfItemType(ItemType.Fish)})")).
                        Concat(DepleteNet(character)),
                    {
                        (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                        (TRY_AGAIN_CHOICE, CATCH_ANOTHER_TEXT, Function() Perform(character), CanPerform(character))
                    },
                    Function() Nothing)
    End Function

    Private Shared Function DepleteNet(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim lines As New List(Of IDialogLine)
        Dim net = character.GetItemOfType(ItemType.FishingNet)
        net.ChangeStatistic(StatisticType.Durability, -1)
        Dim netRuined = net.IsStatisticAtMinimum(StatisticType.Durability)
        If netRuined Then
            lines.Add(
            New DialogLine(
                MoodType.Info,
                $"Yer {net.Name} is ruined!"))
            character.RemoveAndRecycleItem(net)
        Else
            lines.Add(
                New DialogLine(
                    MoodType.Info,
                    $"-1 {StatisticType.Durability.ToStatisticTypeDescriptor.StatisticTypeName} {net.Name}"))
            If net.GetStatistic(StatisticType.Durability) < net.GetStatisticMaximum(StatisticType.Durability) \ 3 Then
                lines.Add(
                New DialogLine(
                    MoodType.Warning,
                    $"Repair yer {net.Name} soon."))
            End If
        End If
        Return lines
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        If Not MyBase.CanPerform(character) Then
            Return False
        End If
        If Not character.GetBumpLocation().LocationType = Business.LocationType.Water Then
            Return False
        End If
        If Not character.HasItemsOfType(ItemType.FishingNet) Then
            Return False
        End If
        Return True
    End Function
End Class
