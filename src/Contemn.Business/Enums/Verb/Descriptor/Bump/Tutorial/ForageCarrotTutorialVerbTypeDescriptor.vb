Friend Class ForageCarrotTutorialVerbTypeDescriptor
    Inherits ForageTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(ForageCarrotTutorialVerbTypeDescriptor),
            "Tutorial: Forage for Carrot",
            NameOf(CarrotItemTypeDescriptor),
            TagType.CompletedForageCarrotTutorial,
            Function(x) True,
            {
                New DialogLine(MoodType.Info, "Forage for Carrot!"),
                New DialogLine(MoodType.Info, "1. Move onto Grass"),
                New DialogLine(MoodType.Info, "2. Press <ACTION>"),
                New DialogLine(MoodType.Info, "3. Select ""Forage..."""),
                New DialogLine(MoodType.Info, "4. Repeat until you get some Carrot"),
                New DialogLine(MoodType.Info, "5. Come back")
            },
            {
                New DialogLine(MoodType.Info, "You can eat carrots.")
            })
    End Sub
End Class
