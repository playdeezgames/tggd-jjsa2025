Friend Class KnifeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(KnifeRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Stick, 1},
                {ItemType.Twine, 1},
                {ItemType.Blade, 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {ItemType.Knife, 1}
            })
    End Sub
End Class
