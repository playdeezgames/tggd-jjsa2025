Imports TGGD.Business

Friend Class FillVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FillVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Fill...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Return ChooseFillVesselDialog.LaunchMenu(character).Invoke
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.CanFill) AndAlso
            character.Items.Any(AddressOf IsFillableItem)
    End Function

    Private Function IsFillableItem(item As IItem) As Boolean
        Return item.GetTag(TagType.IsFillable) AndAlso Not item.IsStatisticAtMaximum(StatisticType.Water)
    End Function
End Class
