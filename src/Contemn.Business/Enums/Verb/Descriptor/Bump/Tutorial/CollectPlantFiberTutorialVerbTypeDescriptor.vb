Imports TGGD.Business

Friend Class CollectPlantFiberTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            "Forage for Plant Fiber",
            NameOf(PlantFiberItemTypeDescriptor),
            TagType.CompletedCollectPlantFiberTutorial,
            Array.Empty(Of String),
            {
                "Move onto Grass",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Plant Fiber",
                "Come back"
            },
            {
                "Plant Fiber is used to Craft things."
            })
    End Sub
End Class
