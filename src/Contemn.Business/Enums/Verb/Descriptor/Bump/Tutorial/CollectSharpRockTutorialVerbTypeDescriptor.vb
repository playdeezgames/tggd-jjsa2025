Friend Class CollectSharpRockTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectSharpRockTutorialVerbTypeDescriptor),
            "Craft Sharp Rock",
            NameOf(SharpRockItemTypeDescriptor),
            TagType.CompletedCollectSharpRockTutorial,
            {TagType.CompletedCollectHammerTutorial},
            {
                "Gather Rock and Hammer",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Sharp Rock recipe",
                "Come Back"
            },
            {
                "Sharp Rock is used to Craft."
            })
    End Sub
End Class
