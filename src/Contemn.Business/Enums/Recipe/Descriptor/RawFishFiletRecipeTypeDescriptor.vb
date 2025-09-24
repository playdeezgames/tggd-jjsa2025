Friend Class RawFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(RawFishFiletRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FishItemTypeDescriptor), 1},
                {NameOf(KnifeItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(RawFishFiletItemTypeDescriptor), 2},
                {NameOf(KnifeItemTypeDescriptor), 1}
            })
    End Sub
End Class
