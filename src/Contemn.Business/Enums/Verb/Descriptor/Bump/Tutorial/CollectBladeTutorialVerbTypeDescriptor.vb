Friend Class CollectBladeTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectBladeTutorialVerbTypeDescriptor),
            "Craft Blade",
            NameOf(BladeItemTypeDescriptor),
            TagType.CompletedCollectBladeTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather Sharp Rock and Hammer",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Blade recipe",
                "Come Back"
            },
            {
                New DialogLine(MoodType.Info, "Blade is used to Craft.")
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectSharpRockTutorial)
    End Function
End Class
