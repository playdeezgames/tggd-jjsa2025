Imports TGGD.Business

Friend Class FillVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.VerbType.Fill,
            Business.VerbCategoryType.Bump,
            "Fill...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim item = character.Items.First(AddressOf GetFillableItem)
        item.SetStatistic(StatisticType.Water, item.GetStatisticMaximum(StatisticType.Water))
        item.SetTag(TagType.Safe, False)
        Return New OkDialog(
            character.World.ProcessTurn().
            Append(New DialogLine(MoodType.Info, $"Filled {item.Name}")),
            CharacterActionsDialog.LaunchMenu(character))
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.CanFill) AndAlso
            character.Items.Any(AddressOf GetFillableItem)
    End Function

    Private Function GetFillableItem(item As IItem) As Boolean
        Return item.GetTag(TagType.IsFillable) AndAlso Not item.IsStatisticAtMaximum(StatisticType.Water)
    End Function
End Class
