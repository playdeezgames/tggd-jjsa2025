Friend Class CollectRawFishFiletTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectRawFishFiletTutorialVerbTypeDescriptor),
            "Craft Filet",
            NameOf(RawFishFiletItemTypeDescriptor),
            TagType.CompletedCollectRawFishFiletTutorial,
            {TagType.CompletedCollectKnifeTutorial, TagType.CompletedCollectFishTutorial},
            {
                "Gather Knife and Fish",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Filet recipe",
                "Come Back"
            },
            {
                "Filet can be cooked."
            })
    End Sub
End Class
