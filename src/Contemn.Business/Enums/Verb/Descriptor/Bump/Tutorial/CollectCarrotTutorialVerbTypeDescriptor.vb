Friend Class CollectCarrotTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectCarrotTutorialVerbTypeDescriptor),
            "Forage for Carrot",
            NameOf(CarrotItemTypeDescriptor),
            TagType.CompletedCollectCarrotTutorial,
            Function(x) True,
            {
                "Move onto Grass",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Carrot",
                "Come back"
            },
            {
                New DialogLine(MoodType.Info, "You can eat carrots.")
            })
    End Sub
End Class
