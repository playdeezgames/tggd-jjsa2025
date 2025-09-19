Imports TGGD.Business

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.VerbType.Fish,
            Business.VerbCategoryType.Bump,
            "Fish")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim item = character.World.CreateItem(ItemType.Fish, character)
        Return New MessageDialog(
            {
                (MoodType.Info, $"+1 {item.Name}({character.GetCountOfItemType(ItemType.Fish)})")
            },
            {
                (OK_CHOICE, OK_TEXT, Function() Nothing, True)
            },
            Function() Nothing)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        If Not character.GetBumpLocation().LocationType = Business.LocationType.Water Then
            Return False
        End If
        If Not character.HasItemsOfType(ItemType.FishingNet) Then
            Return False
        End If
        Return True
    End Function
End Class
