Friend Class DismantleHammerRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(DismantleHammerRecipeTypeDescriptor),
            True,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(HammerItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
