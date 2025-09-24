Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(TwineRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(PlantFiberItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TwineItemTypeDescriptor), 1}
            })
    End Sub
End Class
