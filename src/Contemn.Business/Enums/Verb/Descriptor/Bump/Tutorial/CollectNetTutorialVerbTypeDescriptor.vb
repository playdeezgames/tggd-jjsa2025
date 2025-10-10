Friend Class CollectNetTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectNetTutorialVerbTypeDescriptor),
            "Craft Net",
            NameOf(FishingNetItemTypeDescriptor),
            TagType.CompletedCollectNetTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather sufficient Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Net recipe",
                "Come Back"
            },
            {
                "Net is used to Fish."
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectTwineTutorial)
    End Function
End Class
