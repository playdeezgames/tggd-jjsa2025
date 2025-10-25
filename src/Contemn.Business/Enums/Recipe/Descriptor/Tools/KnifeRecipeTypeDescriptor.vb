Friend Class KnifeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(ShortStickItemTypeDescriptor), 1},
                {NameOf(TwineItemTypeDescriptor), 1},
                {NameOf(BladeItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(KnifeItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
