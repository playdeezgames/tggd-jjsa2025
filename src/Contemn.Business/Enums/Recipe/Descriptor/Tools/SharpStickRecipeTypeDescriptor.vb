Friend Class SharpStickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(SharpStickRecipeTypeDescriptor),
            New Dictionary(Of String, Integer) From
            {
                {NameOf(StickItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {NameOf(SharpStickItemTypeDescriptor), 1}
            },
            New Dictionary(Of String, Integer) From
            {
                {TagType.CanSharpen, 1}
            })
    End Sub
End Class
