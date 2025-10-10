Friend Class CollectKnifeTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectKnifeTutorialVerbTypeDescriptor),
            "Craft Knife",
            NameOf(KnifeItemTypeDescriptor),
            TagType.CompletedCollectKnifeTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Craft Knife!"),
                New DialogLine(MoodType.Info, "1. Gather Blade, Stick, and Twine"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Knife recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Knife is used to Craft.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectBladeTutorial)
    End Function
End Class
