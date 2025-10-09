Imports TGGD.Business

Friend Class BoilVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(BoilVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Boil Water")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Dim messageLines = character.World.ProcessTurn()
        Dim item = character.Items.First(AddressOf IsBoilableItem)
        item.SetTag(TagType.Safe, True)
        Return New OkDialog(
            "You boiled it!",
            messageLines.Append(New DialogLine(MoodType.Info, "You boil one pot of water.")),
            Function() New BumpDialog(character))
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.CanBoil) AndAlso
            bumpLocation.GetTag(TagType.IsLit) AndAlso
            character.Items.Any(AddressOf IsBoilableItem)
    End Function

    Private Function IsBoilableItem(item As IItem) As Boolean
        Return item.GetTag(TagType.IsBoilable) AndAlso
            Not item.GetTag(TagType.Safe) AndAlso
            Not item.IsStatisticAtMinimum(StatisticType.Water)
    End Function
End Class
