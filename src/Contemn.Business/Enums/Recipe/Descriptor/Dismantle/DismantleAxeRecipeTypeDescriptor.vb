Friend Class DismantleAxeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(DismantleAxeRecipeTypeDescriptor),
            True,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(AxeItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
