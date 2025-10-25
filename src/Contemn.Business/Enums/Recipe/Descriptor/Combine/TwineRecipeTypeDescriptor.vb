Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(PlantFiberItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TwineItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
