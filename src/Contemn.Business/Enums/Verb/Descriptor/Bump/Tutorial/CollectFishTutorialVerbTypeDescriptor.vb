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
                "Craft a Net",
                "Go to water",
                "Select ""Fish""",
                "Repeat until you have a Fish",
                "Come Back"
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
