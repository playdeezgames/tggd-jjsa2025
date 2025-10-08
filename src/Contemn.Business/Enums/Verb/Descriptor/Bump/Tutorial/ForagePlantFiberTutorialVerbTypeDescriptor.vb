Imports TGGD.Business

Friend Class ForagePlantFiberTutorialVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(ForagePlantFiberTutorialVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Tutorial: Forage for Plant Fiber")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        If character.HasItemsOfType(NameOf(PlantFiberItemTypeDescriptor)) Then
            Return Success(character)
        End If
        Return Failure(character)
    End Function

    Private Function Failure(character As ICharacter) As IDialog
        Return New OkDialog(
            {
                New DialogLine(MoodType.Info, "Forage for Plant Fiber!"),
                New DialogLine(MoodType.Info, "1. Move onto Grass"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Forage..."""),
                New DialogLine(MoodType.Info, "4. Repeat until you get some Plant Fiber"),
                New DialogLine(MoodType.Info, "5. Come back")
            },
            BumpDialog.LaunchMenu(character))
    End Function

    Private Function Success(character As ICharacter) As IDialog
        character.SetTag(TagType.CompletedForagePlantFiberTutorial, True)

        Return New OkDialog(
            {
                New DialogLine(MoodType.Info, "Plant Fiber is used to Craft things.")
            }.Concat(RestoreStats(character)),
            BumpDialog.LaunchMenu(character))
    End Function

    Private Function RestoreStats(character As ICharacter) As IEnumerable(Of DialogLine)
        Return {
                New DialogLine(MoodType.Info, "Reward:"),
                Restore(character, StatisticType.Hydration),
                Restore(character, StatisticType.Satiety)
            }
    End Function

    Private Shared Function Restore(character As ICharacter, statisticType As String) As DialogLine
        Dim delta = character.GetStatisticMaximum(statisticType) - character.GetStatistic(statisticType)
        character.ChangeStatistic(statisticType, delta)
        Return New DialogLine(MoodType.Info, $"+{delta} {character.FormatStatistic(statisticType)}")
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsTutorialHouse) AndAlso
            Not character.GetTag(TagType.CompletedForagePlantFiberTutorial)
    End Function
End Class
