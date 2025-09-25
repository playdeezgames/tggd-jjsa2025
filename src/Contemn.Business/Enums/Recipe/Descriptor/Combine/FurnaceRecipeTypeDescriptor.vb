Friend Class FurnaceRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FurnaceRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(ClayItemTypeDescriptor), 2},
                {NameOf(RockItemTypeDescriptor), 8}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FurnaceItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
