Imports TGGD.Business

Friend Class LightTorchVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(LightTorchVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Light Torch")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Dim messageLines = character.World.ProcessTurn()
        Dim item = character.Items.First(Function(x) x.GetTag(TagType.IsIgnitable) AndAlso Not x.GetTag(TagType.IsLit))
        item.SetTag(TagType.IsLit, True)
        Return New OkDialog(
            messageLines.
            Append(New DialogLine(MoodType.Info, $"You light {item.Name}.")),
            BumpDialog.LaunchMenu(character))
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsLit) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.IsIgnitable) AndAlso Not x.GetTag(TagType.IsLit))
    End Function
End Class
