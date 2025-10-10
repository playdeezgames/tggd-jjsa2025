Friend Class CollectKnifeTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            "Craft Knife",
            NameOf(KnifeItemTypeDescriptor),
            TagType.CompletedCollectKnifeTutorial,
            {TagType.CompletedCollectBladeTutorial},
            {
                "Gather Blade, Stick, and Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Knife recipe",
                "Come Back"
            },
            {
                "Knife is used to Craft."
            })
    End Sub
End Class
