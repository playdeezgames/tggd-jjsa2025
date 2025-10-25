Friend Class TorchRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(ShortStickItemTypeDescriptor), 1},
                {NameOf(PlantFiberItemTypeDescriptor), 5}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TorchItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
