Friend Class SharpRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(SharpRockRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(RockItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(SharpRockItemTypeDescriptor), 1},
                {NameOf(RockItemTypeDescriptor), 1}
            })
    End Sub
End Class
