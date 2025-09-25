Imports TGGD.Business

Friend Class PlaceVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.VerbType.Place,
            Business.VerbCategoryType.Action,
            "Place...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return PlaceItemDialog.LaunchMenu(character)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso
            HasPlaceableItem(character) AndAlso
            HasPlaceableNeighbor(character)
    End Function

    Private Shared Function HasPlaceableNeighbor(character As ICharacter) As Boolean
        Return DirectionTypes.All.Any(Function(x) IsNeighborPlaceable(character.Location, x))
    End Function

    Private Shared Function IsNeighborPlaceable(location As ILocation, direction As String) As Boolean
        Dim nextLocation = location.NextLocation(direction)
        Return nextLocation IsNot Nothing AndAlso nextLocation.GetTag(TagType.IsPlaceable)
    End Function

    Private Shared Function HasPlaceableItem(character As ICharacter) As Boolean
        Return character.Items.Any(Function(x) x.GetTag(TagType.CanPlace))
    End Function
End Class
