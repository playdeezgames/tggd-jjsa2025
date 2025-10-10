Friend Class CollectKnifeTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectKnifeTutorialVerbTypeDescriptor),
            "Craft Knife",
            NameOf(KnifeItemTypeDescriptor),
            TagType.CompletedCollectKnifeTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather Blade, Stick, and Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Knife recipe",
                "Come Back"
            },
            {
                "Knife is used to Craft."
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectBladeTutorial)
    End Function
End Class
