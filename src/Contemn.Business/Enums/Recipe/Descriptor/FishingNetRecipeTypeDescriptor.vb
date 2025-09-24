Friend Class FishingNetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FishingNetRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TwineItemTypeDescriptor), 4}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FishingNetItemTypeDescriptor), 1}
            })
    End Sub
End Class
