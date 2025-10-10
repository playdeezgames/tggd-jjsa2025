Imports TGGD.Business

Friend Class CollectPlantFiberTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectPlantFiberTutorialVerbTypeDescriptor),
            "Forage for Plant Fiber",
            NameOf(PlantFiberItemTypeDescriptor),
            TagType.CompletedCollectPlantFiberTutorial,
            Function(x) True,
            {
                "Move onto Grass",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Plant Fiber",
                "Come back"
            },
            {
                New DialogLine(MoodType.Info, "Plant Fiber is used to Craft things.")
            })
    End Sub
End Class
