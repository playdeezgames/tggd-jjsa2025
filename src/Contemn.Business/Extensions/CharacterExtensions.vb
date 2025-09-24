Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module CharacterExtensions
    <Extension>
    Friend Function HandleBump(character As ICharacter, location As ILocation) As IDialog
        character.SetBumpLocation(location)
        Dim result = character.CharacterType.ToCharacterTypeDescriptor.OnBump(character, location)
        Return result
    End Function
    <Extension>
    Friend Sub SetBumpLocation(character As ICharacter, location As ILocation)
        character.SetStatistic(StatisticType.BumpLocationId, location?.LocationId)
    End Sub
    <Extension>
    Friend Sub RemoveAndRecycleItem(character As ICharacter, item As IItem)
        character.RemoveItem(item)
        item.Recycle()
    End Sub
    <Extension>
    Friend Function GetBumpLocation(character As ICharacter) As ILocation
        If Not character.HasStatistic(StatisticType.BumpLocationId) Then
            Return Nothing
        End If
        Return character.World.GetLocation(character.GetStatistic(StatisticType.BumpLocationId))
    End Function
    <Extension>
    Friend Sub HandleLeave(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnLeave(character, location)
    End Sub
    <Extension>
    Friend Sub HandleEnter(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnEnter(character, location)
    End Sub
    <Extension>
    Friend Function IsDead(character As ICharacter) As Boolean
        Return character.IsStatisticAtMinimum(StatisticType.Health)
    End Function
    <Extension>
    Friend Function CraftRecipe(character As ICharacter, recipeType As String, nextDialog As Func(Of IDialog)) As IDialog
        Dim CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)
        Const CRAFT_ANOTHER_TEXT = "Craft Another"
        Dim descriptor = recipeType.ToRecipeTypeDescriptor
        Dim messageLines = descriptor.Craft(character)
        character.PlaySfx(Sfx.Craft)
        character.ChangeStatistic(StatisticType.Score, 1)
        Return New MessageDialog(
            messageLines,
            {
                (OK_CHOICE, OK_TEXT, nextDialog, True),
                (CRAFT_ANOTHER_CHOICE, CRAFT_ANOTHER_TEXT, Function() character.CraftRecipe(recipeType, nextDialog), descriptor.CanCraft(character))
            },
            nextDialog)
    End Function
    <Extension>
    Friend Function GetDurabilityTotal(character As ICharacter, tag As String) As Integer
        Return character.Items.Where(Function(x) x.GetTag(tag)).Sum(Function(x) x.GetStatistic(StatisticType.Durability))
    End Function
End Module
