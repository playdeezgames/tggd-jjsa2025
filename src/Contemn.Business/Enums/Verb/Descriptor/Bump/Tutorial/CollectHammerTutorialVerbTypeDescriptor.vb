Friend Class CollectHammerTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectHammerTutorialVerbTypeDescriptor),
            "Craft Hammer",
            NameOf(HammerItemTypeDescriptor),
            TagType.CompletedCollectHammerTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Craft Hammer!"),
                New DialogLine(MoodType.Info, "1. Gather Stick, Rock, Twine"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Hammer recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Hammer is used to Craft.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectTwineTutorial) AndAlso
            character.GetTag(TagType.CompletedCollectRockTutorial) AndAlso
            character.GetTag(TagType.CompletedCollectStickTutorial)
    End Function
End Class
