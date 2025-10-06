Imports TGGD.Business

Friend Class CookVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(CookVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Cook...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Return CookItemDialog.LaunchMenu(character).Invoke()
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.CanCook) AndAlso
            bumpLocation.GetTag(TagType.IsLit) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.IsCookable))
    End Function
End Class
