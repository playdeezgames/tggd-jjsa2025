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
                New DialogLine(MoodType.Info, "Craft Filet!"),
                New DialogLine(MoodType.Info, "1. Gather Knife and Fish"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Filet recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
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
