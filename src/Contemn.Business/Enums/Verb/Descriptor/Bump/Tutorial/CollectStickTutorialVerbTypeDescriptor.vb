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
                "Stick are used to Craft.",
                "They may be used as fuel."
            })
    End Sub
End Class
