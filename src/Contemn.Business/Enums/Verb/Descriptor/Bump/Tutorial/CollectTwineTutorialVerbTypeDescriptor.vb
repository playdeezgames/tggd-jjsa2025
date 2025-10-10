Friend Class CollectTwineTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectTwineTutorialVerbTypeDescriptor),
            "Craft Twine",
            NameOf(TwineItemTypeDescriptor),
            TagType.CompletedCollectTwineTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Craft Twine!"),
                New DialogLine(MoodType.Info, "1. Gather sufficient Plant Fiber"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Twine recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Twine is used to Craft.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectPlantFiberTutorial)
    End Function
End Class
