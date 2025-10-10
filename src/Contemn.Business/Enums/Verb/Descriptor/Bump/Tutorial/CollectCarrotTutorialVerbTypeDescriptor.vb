Friend Class CollectCarrotTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectCarrotTutorialVerbTypeDescriptor),
            "Forage for Carrot",
            NameOf(CarrotItemTypeDescriptor),
            TagType.CompletedCollectCarrotTutorial,
            Array.Empty(Of String),
            {
                "Move onto Grass",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Carrot",
                "Come back"
            },
            {
                "You can eat carrots."
            })
    End Sub
End Class
