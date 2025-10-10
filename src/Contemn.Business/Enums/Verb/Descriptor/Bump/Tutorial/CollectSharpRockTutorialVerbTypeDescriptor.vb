Friend Class CollectSharpRockTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectSharpRockTutorialVerbTypeDescriptor),
            "Tutorial: Craft Sharp Rock",
            NameOf(SharpRockItemTypeDescriptor),
            TagType.CompletedCollectSharpRockTutorial,
            AddressOf HasPrerequisites,
            {
                New DialogLine(MoodType.Info, "Craft Sharp Rock!"),
                New DialogLine(MoodType.Info, "1. Gather Rock and Hammer"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Craft..."""),
                New DialogLine(MoodType.Info, "4. Select Sharp Rock recipe"),
                New DialogLine(MoodType.Info, "5. Come Back")
            },
            {
                New DialogLine(MoodType.Info, "Sharp Rock is used to Craft.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectHammerTutorial)
    End Function
End Class
