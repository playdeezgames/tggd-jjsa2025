Imports TGGD.Business

Friend Class ExtinguishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(ExtinguishVerbTypeDescriptor), Business.VerbCategoryType.Bump, "Extinguish")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        character.GetBumpLocation.SetTag(TagType.IsLit, False)
        Return BumpDialog.LaunchMenu(character).Invoke
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsLit)
    End Function
End Class
