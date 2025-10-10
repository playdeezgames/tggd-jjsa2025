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
                "Fish is food, not friends.",
                "Raw fish may cause food poisoning.",
                "Proper preparation and cooking is key."
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectNetTutorial)
    End Function
End Class
