Friend Class DismantleTwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(DismantleTwineRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TwineItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(PlantFiberItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
