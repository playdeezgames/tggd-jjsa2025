Friend Class ShortStickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(ShortStickRecipeTypeDescriptor),
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(ShortStickItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {TagType.CanChop, 1}
            })
    End Sub
End Class
