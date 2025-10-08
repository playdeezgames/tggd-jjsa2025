Friend Class CollectRockTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectRockTutorialVerbTypeDescriptor),
            "Tutorial: Forage for Rock",
            NameOf(RockItemTypeDescriptor),
            TagType.CompletedCollectRockTutorial,
            Function(x) True,
            {
                New DialogLine(MoodType.Info, "Forage for Rock!"),
                New DialogLine(MoodType.Info, "1. Move onto Rock"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Forage..."""),
                New DialogLine(MoodType.Info, "4. Repeat until you get some Rock"),
                New DialogLine(MoodType.Info, "5. Come back")
            },
            {
                New DialogLine(MoodType.Info, "Rocks are used to Craft.")
            })
    End Sub
End Class
