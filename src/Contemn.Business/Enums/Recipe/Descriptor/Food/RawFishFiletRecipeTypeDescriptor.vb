Friend Class RawFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            False,
            New Dictionary(Of String, Integer) From
            {
                {NameOf(FishItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(RawFishFiletItemTypeDescriptor), 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {TagType.CanCut, 1}
            })
    End Sub
End Class
