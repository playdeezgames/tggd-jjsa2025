Friend Class CollectHammerTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            "Craft Hammer",
            NameOf(HammerItemTypeDescriptor),
            TagType.CompletedCollectHammerTutorial,
            {
                TagType.CompletedCollectTwineTutorial,
                TagType.CompletedCollectRockTutorial,
                TagType.CompletedCollectStickTutorial
            },
            {
                "Gather Stick, Rock, Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Hammer recipe",
                "Come Back"
            },
            {
                "Hammer is used to Craft."
            })
    End Sub
End Class
