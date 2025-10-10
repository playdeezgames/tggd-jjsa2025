Friend Class CollectNetTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            "Craft Net",
            NameOf(FishingNetItemTypeDescriptor),
            TagType.CompletedCollectNetTutorial,
            {TagType.CompletedCollectTwineTutorial},
            {
                "Gather sufficient Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Net recipe",
                "Come Back"
            },
            {
                "Net is used to Fish."
            })
    End Sub
End Class
