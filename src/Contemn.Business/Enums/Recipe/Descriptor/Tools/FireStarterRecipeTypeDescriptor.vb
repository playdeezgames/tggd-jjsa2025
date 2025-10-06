Friend Class FireStarterRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FireStarterRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 2},
                {NameOf(PlantFiberItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FireStarterItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
