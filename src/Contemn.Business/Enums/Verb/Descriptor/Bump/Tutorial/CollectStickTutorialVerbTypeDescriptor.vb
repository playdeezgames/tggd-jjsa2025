Friend Class CollectStickTutorialVerbTypeDescriptor
    Inherits CollectItemTypeTutorialVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CollectStickTutorialVerbTypeDescriptor),
            "Collect Stick",
            NameOf(StickItemTypeDescriptor),
            TagType.CompletedCollectStickTutorial,
            Function(x) True,
            {
                "Move into Tree",
                "Select ""Collect Stick""",
                "Come back"
            },
            {
                New DialogLine(MoodType.Info, "Stick are used to Craft."),
                New DialogLine(MoodType.Info, "They may be used as fuel.")
            })
    End Sub
End Class
