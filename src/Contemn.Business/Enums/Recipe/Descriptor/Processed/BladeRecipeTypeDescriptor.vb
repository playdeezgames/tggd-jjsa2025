Friend Class BladeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From {
                {NameOf(SharpRockItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From {
                {NameOf(BladeItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer) From {
                {TagType.CanHammer, 1}
            })
    End Sub
End Class
