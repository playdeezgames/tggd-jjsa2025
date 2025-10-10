Friend Class CollectSharpRockTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectSharpRockTutorialVerbTypeDescriptor),
            "Craft Sharp Rock",
            NameOf(SharpRockItemTypeDescriptor),
            TagType.CompletedCollectSharpRockTutorial,
            AddressOf HasPrerequisites,
            {
                "Gather Rock and Hammer",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Sharp Rock recipe",
                "Come Back"
            },
            {
                "Sharp Rock is used to Craft."
            })
    End Sub

    Private Shared Function HasPrerequisites(character As ICharacter) As Boolean
        Return character.GetTag(TagType.CompletedCollectHammerTutorial)
    End Function
End Class
