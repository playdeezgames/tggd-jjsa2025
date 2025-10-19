Friend Class BowDrillRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(BowDrillRecipeTypeDescriptor),
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FireBowItemTypeDescriptor), 1},
                {NameOf(PlankItemTypeDescriptor), 1},
                {NameOf(ShortStickItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(BowDrillItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
