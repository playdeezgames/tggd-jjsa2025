Friend Class DismantleFishingNetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(DismantleFishingNetRecipeTypeDescriptor),
            True,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FishingNetItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TwineItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
