Friend Class CollectRawFishFiletTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectRawFishFiletTutorialVerbTypeDescriptor),
            "Craft Filet",
            NameOf(RawFishFiletItemTypeDescriptor),
            TagType.CompletedCollectRawFishFiletTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather Knife and Fish",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Filet recipe",
                "Come Back"
            },
            {
                New DialogLine(MoodType.Info, "Filet can be cooked.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectKnifeTutorial) AndAlso
            character.GetTag(TagType.CompletedCollectFishTutorial)
    End Function
End Class
