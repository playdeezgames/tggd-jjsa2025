Friend Class CampFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CampFireRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 8},
                {NameOf(RockItemTypeDescriptor), 8}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(CampFireItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
