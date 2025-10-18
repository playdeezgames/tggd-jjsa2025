Friend Class PlankRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(PlankRecipeTypeDescriptor),
            False,
            New Dictionary(Of String, Integer) From {
                {NameOf(LogItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From {
                {NameOf(PlankItemTypeDescriptor), 4}
            },
            New Dictionary(Of String, Integer) From {
                {TagType.CanHammer, 1},
                {TagType.CanChop, 1}
            })
    End Sub
End Class
