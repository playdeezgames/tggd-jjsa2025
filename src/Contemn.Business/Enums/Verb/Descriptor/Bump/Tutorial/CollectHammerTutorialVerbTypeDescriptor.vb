Friend Class CollectHammerTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectHammerTutorialVerbTypeDescriptor),
            "Craft Hammer",
            NameOf(HammerItemTypeDescriptor),
            TagType.CompletedCollectHammerTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather Stick, Rock, Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Hammer recipe",
                "Come Back"
            },
            {
                "Hammer is used to Craft."
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectTwineTutorial) AndAlso
            character.GetTag(TagType.CompletedCollectRockTutorial) AndAlso
            character.GetTag(TagType.CompletedCollectStickTutorial)
    End Function
End Class
