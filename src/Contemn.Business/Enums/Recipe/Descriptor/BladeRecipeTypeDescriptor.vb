Friend Class BladeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(BladeRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From {
                {ItemType.Rock, 1},
                {ItemType.SharpRock, 1}
            },
            New Dictionary(Of String, Integer) From {
                {ItemType.Rock, 1},
                {ItemType.Blade, 2}
            })
    End Sub
End Class
