Friend Class CollectTwineTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            "Craft Twine",
            NameOf(TwineItemTypeDescriptor),
            TagType.CompletedCollectTwineTutorial,
            {TagType.CompletedCollectPlantFiberTutorial},
            {
                "Gather sufficient Plant Fiber",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Twine recipe",
                "Come Back"
            },
            {
                "Twine is used to Craft."
            })
    End Sub
End Class
