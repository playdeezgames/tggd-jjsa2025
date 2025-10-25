Friend Module RecipeTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of Integer, RecipeTypeDescriptor) =
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
            New FireStarterRecipeTypeDescriptor(),
            New TorchRecipeTypeDescriptor(),
            New FireBowRecipeTypeDescriptor(),
            New ShortStickRecipeTypeDescriptor(),
            New PlankRecipeTypeDescriptor(),
            New BowDrillRecipeTypeDescriptor()
        }.ToDictionary(Function(x) x.RecipeId, Function(x) x)
End Module
