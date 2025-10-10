Friend Class CollectRockTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectRockTutorialVerbTypeDescriptor),
            "Forage for Rock",
            NameOf(RockItemTypeDescriptor),
            TagType.CompletedCollectRockTutorial,
            Function(x) True,
            {
                "Move onto Rock",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Rock",
                "Come back"
            },
            {
                "Rocks are used to Craft."
            })
    End Sub
End Class
