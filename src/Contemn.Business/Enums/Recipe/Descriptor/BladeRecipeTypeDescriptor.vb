Friend Class BladeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(BladeRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From {
                {NameOf(RockItemTypeDescriptor), 1},
                {NameOf(SharpRockItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From {
                {NameOf(RockItemTypeDescriptor), 1},
                {NameOf(BladeItemTypeDescriptor), 2}
            })
    End Sub
End Class
