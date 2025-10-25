Friend Class FishingNetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(TwineItemTypeDescriptor), 4}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FishingNetItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer))
    End Sub
End Class
