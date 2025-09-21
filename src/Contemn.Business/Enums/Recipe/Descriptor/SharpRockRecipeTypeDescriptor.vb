Friend Class SharpRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(SharpRockRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Rock, 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {ItemType.SharpRock, 1},
                {ItemType.Rock, 1}
            })
    End Sub
End Class
