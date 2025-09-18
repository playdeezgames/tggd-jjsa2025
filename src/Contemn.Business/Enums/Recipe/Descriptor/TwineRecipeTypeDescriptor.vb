Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            New Dictionary(Of String, Integer) From
            {
                {ItemType.PlantFiber, 2}
            },
            New Dictionary(Of String, Integer) From {})
    End Sub
End Class
