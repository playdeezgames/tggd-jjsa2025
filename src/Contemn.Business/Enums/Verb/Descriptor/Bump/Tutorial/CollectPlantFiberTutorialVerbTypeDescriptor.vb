Imports TGGD.Business

Friend Class CollectPlantFiberTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectPlantFiberTutorialVerbTypeDescriptor),
            "Tutorial: Forage for Plant Fiber",
            NameOf(PlantFiberItemTypeDescriptor),
            TagType.CompletedCollectPlantFiberTutorial,
            Function(x) True,
            {
                New DialogLine(MoodType.Info, "Forage for Plant Fiber!"),
                New DialogLine(MoodType.Info, "1. Move onto Grass"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Forage..."""),
                New DialogLine(MoodType.Info, "4. Repeat until you get some Plant Fiber"),
                New DialogLine(MoodType.Info, "5. Come back")
            },
            {
                New DialogLine(MoodType.Info, "Plant Fiber is used to Craft things.")
            })
    End Sub
End Class
