Friend Class SharpRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(SharpRockRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(RockItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(SharpRockItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {TagType.CanHammer, 1}
            })
    End Sub
End Class
