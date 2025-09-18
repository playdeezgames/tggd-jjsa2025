Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(RecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {ItemType.PlantFiber, 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Twine, 1}
            })
    End Sub
End Class
