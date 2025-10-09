Imports TGGD.Business

Friend Class LightVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(LightVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Light")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation
        Dim item = character.Items.First(Function(x) x.GetTag(TagType.CanLight))
        Dim messageLines = character.World.ProcessTurn().
            Concat(item.Deplete(character)).
            Append(New DialogLine(MoodType.Info, $"You light the {bumpLocation.Name}"))
        bumpLocation.SetTag(TagType.IsLit, True)
        Return New OkDialog(
            "You lit it!",
            messageLines,
            BumpDialog.LaunchMenu(character))
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsIgnitable) AndAlso
            Not bumpLocation.GetTag(TagType.IsLit) AndAlso
            Not bumpLocation.IsStatisticAtMinimum(StatisticType.Fuel) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanLight))
    End Function
End Class
