Friend Class CollectFishTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectFishTutorialVerbTypeDescriptor),
            "Catch Fish!",
            NameOf(FishItemTypeDescriptor),
            TagType.CompletedCollectFishTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Catch Fish!"),
                New DialogLine(MoodType.Info, "1. Craft a Net"),
                New DialogLine(MoodType.Info, "2. Go to water"),
                New DialogLine(MoodType.Info, "3. Select ""Fish"""),
                New DialogLine(MoodType.Info, "4. Repeat until you have a Fish"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Fish is food, not friends."),
                New DialogLine(MoodType.Info, "Raw fish may cause food poisoning."),
                New DialogLine(MoodType.Info, "Proper preparation and cooking is key.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectNetTutorial)
    End Function
End Class
