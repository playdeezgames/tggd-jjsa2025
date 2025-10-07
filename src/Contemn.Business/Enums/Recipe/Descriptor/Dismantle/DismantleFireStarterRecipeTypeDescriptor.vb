Friend Class DismantleFireStarterRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(DismantleFireStarterRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FireStarterItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
