Friend Class KnifeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(KnifeRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1},
                {NameOf(TwineItemTypeDescriptor), 1},
                {NameOf(BladeItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(KnifeItemTypeDescriptor), 1}
            })
    End Sub
End Class
