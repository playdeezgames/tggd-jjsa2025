Friend Module RecipeTypes
    Friend ReadOnly Descriptors As IEnumerable(Of RecipeTypeDescriptor) =
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
        }
End Module
