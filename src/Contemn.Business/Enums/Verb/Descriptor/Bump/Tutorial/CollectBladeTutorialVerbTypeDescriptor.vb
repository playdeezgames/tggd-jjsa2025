Friend Class CollectBladeTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectBladeTutorialVerbTypeDescriptor),
            "Craft Blade",
            NameOf(BladeItemTypeDescriptor),
            TagType.CompletedCollectBladeTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Craft Blade!"),
                New DialogLine(MoodType.Info, "1. Gather Sharp Rock and Hammer"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Blade recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Blade is used to Craft.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectSharpRockTutorial)
    End Function
End Class
