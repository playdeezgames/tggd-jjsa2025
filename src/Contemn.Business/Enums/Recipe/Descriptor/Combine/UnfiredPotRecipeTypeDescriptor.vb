Friend Class UnfiredPotRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(ClayItemTypeDescriptor), 3}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(UnfiredPotItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
