Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module CharacterExtensions
    <Extension>
    Friend Sub SetBumpLocation(
                              character As ICharacter,
                              location As ILocation)
        character.SetStatistic(StatisticType.BumpLocationId, location?.LocationId)
    End Sub
    <Extension>
    Friend Function GetBumpLocation(
                                   character As ICharacter) As ILocation
        If Not character.HasStatistic(StatisticType.BumpLocationId) Then
            Return Nothing
        End If
        Return character.World.GetLocation(character.GetStatistic(StatisticType.BumpLocationId))
    End Function
    <Extension>
    Friend Sub HandleLeave(
                          character As ICharacter,
                          location As ILocation)
        character.Descriptor.OnLeave(character, location)
    End Sub
    <Extension>
    Friend Sub HandleEnter(
                          character As ICharacter,
                          location As ILocation)
        character.Descriptor.OnEnter(character, location)
    End Sub
    <Extension>
    Friend Function IsDead(
                          character As ICharacter) As Boolean
        Return character.IsStatisticAtMinimum(StatisticType.Health)
    End Function
    <Extension>
    Friend Function CraftRecipe(
                               character As ICharacter,
                               descriptor As RecipeTypeDescriptor,
                               nextDialog As Func(Of IDialog),
                               confirmed As Boolean) As IDialog
        If descriptor.IsDestructive AndAlso Not confirmed Then
            Return New ConfirmDialog(
                "Are you sure?",
                {
                    New DialogLine(MoodType.Warning, "This recipe is destructive."),
                    New DialogLine(MoodType.Info, "Please confirm.")
                },
                Function() character.CraftRecipe(descriptor, nextDialog, True),
                nextDialog)
        Else
            Dim CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)
            Const CRAFT_ANOTHER_TEXT = "Craft Another"
            Dim messageLines = descriptor.Craft(character)
            character.Platform.PlaySfx(Sfx.Craft)
            character.ChangeStatistic(StatisticType.Score, 1)
            Return New MessageDialog(
            "Behold!",
            messageLines,
            {
                (OK_CHOICE, OK_TEXT, nextDialog, True),
                (CRAFT_ANOTHER_CHOICE, CRAFT_ANOTHER_TEXT, Function() character.CraftRecipe(descriptor, nextDialog, True), descriptor.CanCraft(character))
            },
            nextDialog)
        End If
    End Function
    <Extension>
    Friend Function GetDurabilityTotal(
                                      character As ICharacter,
                                      tag As String) As Integer
        Return character.Items.Where(Function(x) x.GetTag(tag)).Sum(Function(x) x.GetStatistic(StatisticType.Durability))
    End Function
End Module
