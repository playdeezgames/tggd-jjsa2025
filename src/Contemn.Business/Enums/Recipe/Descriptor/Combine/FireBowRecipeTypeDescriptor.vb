Friend Class FireBowRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(FireBowItemTypeDescriptor),
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1},
                {NameOf(TwineItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FireBowItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
