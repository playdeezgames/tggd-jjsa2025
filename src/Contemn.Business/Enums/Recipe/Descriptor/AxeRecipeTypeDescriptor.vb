Friend Class AxeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(AxeRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Stick, 1},
                {ItemType.Twine, 1},
                {ItemType.SharpRock, 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Axe, 1}
            })
    End Sub
End Class
