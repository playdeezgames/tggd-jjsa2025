Friend Class TorchRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(TorchRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1},
                {NameOf(PlantFiberItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TorchItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
