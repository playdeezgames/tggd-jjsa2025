Friend Class CollectTwineTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectTwineTutorialVerbTypeDescriptor),
            "Craft Twine",
            NameOf(TwineItemTypeDescriptor),
            TagType.CompletedCollectTwineTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather sufficient Plant Fiber",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Twine recipe",
                "Come Back"
            },
            {
                "Twine is used to Craft."
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectPlantFiberTutorial)
    End Function
End Class
