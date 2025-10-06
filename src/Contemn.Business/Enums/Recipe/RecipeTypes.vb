Imports System.Runtime.CompilerServices

Friend Module RecipeTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, RecipeTypeDescriptor) =
        New List(Of RecipeTypeDescriptor) From
        {
            New TwineRecipeTypeDescriptor(),
            New FishingNetRecipeTypeDescriptor(),
            New SharpRockRecipeTypeDescriptor(),
            New AxeRecipeTypeDescriptor(),
            New BladeRecipeTypeDescriptor(),
            New KnifeRecipeTypeDescriptor(),
            New RawFishFiletRecipeTypeDescriptor(),
            New HammerRecipeTypeDescriptor(),
            New SharpStickRecipeTypeDescriptor(),
            New UnfiredPotRecipeTypeDescriptor(),
            New CampFireRecipeTypeDescriptor(),
            New KilnRecipeTypeDescriptor(),
            New FireStarterRecipeTypeDescriptor()
        }.ToDictionary(
            Function(x) x.RecipeType,
            Function(x) x)
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    <Extension>
    Function ToRecipeTypeDescriptor(recipeType As String) As RecipeTypeDescriptor
        Return Descriptors(recipeType)
    End Function
End Module
