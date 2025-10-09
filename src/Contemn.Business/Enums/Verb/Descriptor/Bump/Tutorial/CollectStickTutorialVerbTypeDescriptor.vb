Friend Class CollectStickTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectStickTutorialVerbTypeDescriptor),
            "Tutorial: Collect Stick",
            NameOf(StickItemTypeDescriptor),
            TagType.CompletedCollectStickTutorial,
            Function(x) True,
            {
                New DialogLine(MoodType.Info, "Collect Stick!"),
                New DialogLine(MoodType.Info, "1. Move into Tree"),
                New DialogLine(MoodType.Info, "2. Select ""Collect Stick"""),
                New DialogLine(MoodType.Info, "3. Come back")
            },
            {
                New DialogLine(MoodType.Info, "Stick are used to Craft."),
                New DialogLine(MoodType.Info, "They may be used as fuel.")
            })
    End Sub
End Class
