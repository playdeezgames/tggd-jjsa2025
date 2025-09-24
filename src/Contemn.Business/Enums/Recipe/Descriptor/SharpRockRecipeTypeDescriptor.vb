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
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
