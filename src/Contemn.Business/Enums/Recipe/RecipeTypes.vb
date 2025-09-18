Friend Module RecipeTypes
    Friend ReadOnly Descriptors As IReadOnlyList(Of RecipeTypeDescriptor) =
        New List(Of RecipeTypeDescriptor) From
        {
            New TwineRecipeTypeDescriptor()
        }
End Module
