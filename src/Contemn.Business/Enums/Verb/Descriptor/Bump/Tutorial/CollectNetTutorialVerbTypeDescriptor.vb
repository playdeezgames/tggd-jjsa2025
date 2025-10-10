Friend Class CollectNetTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectNetTutorialVerbTypeDescriptor),
            "Craft Net",
            NameOf(FishingNetItemTypeDescriptor),
            TagType.CompletedCollectNetTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Craft Net!"),
                New DialogLine(MoodType.Info, "1. Gather sufficient Twine"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Net recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Net is used to Fish.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectTwineTutorial)
    End Function
End Class
