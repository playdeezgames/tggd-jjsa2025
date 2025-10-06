Imports TGGD.Business

Friend Class RefuelVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(RefuelVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Refuel...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Return RefuelItemDialog.LaunchMenu(character).Invoke()
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.IsRefuelable) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanRefuel))
    End Function
End Class
