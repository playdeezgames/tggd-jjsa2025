Imports TGGD.Business

Friend Class LightVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(LightVerbTypeDescriptor), Business.VerbCategoryType.Bump, "Light")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        character.GetBumpLocation.SetTag(TagType.IsLit, True)
        Return BumpDialog.LaunchMenu(character).Invoke
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsLightable) AndAlso
            Not bumpLocation.GetTag(TagType.IsLit) AndAlso
            Not bumpLocation.IsStatisticAtMinimum(StatisticType.Fuel)
    End Function
End Class
