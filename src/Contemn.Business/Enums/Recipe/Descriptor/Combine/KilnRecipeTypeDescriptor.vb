Friend Class KilnRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(ClayItemTypeDescriptor), 2},
                {NameOf(RockItemTypeDescriptor), 8}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(KilnItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
