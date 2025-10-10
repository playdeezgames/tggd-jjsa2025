Friend Class CollectBladeTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectBladeTutorialVerbTypeDescriptor),
            "Craft Blade",
            NameOf(BladeItemTypeDescriptor),
            TagType.CompletedCollectBladeTutorial,
            {TagType.CompletedCollectSharpRockTutorial},
            {
                "Gather Sharp Rock and Hammer",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Blade recipe",
                "Come Back"
            },
            {
                "Blade is used to Craft."
            })
    End Sub
End Class
