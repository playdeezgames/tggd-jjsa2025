Friend Class FishingNetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FishingNetRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Twine, 4}
            },
            New Dictionary(Of String, Integer) From
            {
                {ItemType.FishingNet, 1}
            })
    End Sub
End Class
