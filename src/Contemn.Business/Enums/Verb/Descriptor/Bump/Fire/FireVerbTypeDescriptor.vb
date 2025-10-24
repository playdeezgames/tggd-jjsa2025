Imports TGGD.Business

Friend Class FireVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FireVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Fire Pot...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        Dim unfiredItem = character.Items.First(Function(x) x.GetTag(TagType.CanKiln))
        Dim unfiredItemType = unfiredItem.ItemType
        Dim firedItem = character.World.CreateItem(unfiredItem.GetMetadata(MetadataType.FiredItemType), character)
        character.RemoveAndRecycleItem(unfiredItem)
        Return New OkDialog(
            "You fired it!",
            character.World.ProcessTurn().
            Append(New DialogLine(MoodType.Info, $"-1 {ItemTypes.Descriptors(unfiredItemType).ItemTypeName}({character.GetCountOfItemType(unfiredItemType)})")).
            Append(New DialogLine(MoodType.Info, $"+1 {firedItem.Name}({character.GetCountOfItemType(firedItem.ItemType)})")),
            AddressOf character.ActionMenu)
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsKiln) AndAlso
            bumpLocation.GetTag(TagType.IsLit) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanKiln))
    End Function
End Class
