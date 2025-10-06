Friend Class CraftVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(CraftVerbTypeDescriptor), Business.VerbCategoryType.Action, "Craft...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As TGGD.Business.IDialog
        Return CraftDialog.LaunchMenu(character).Invoke
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso character.HasAvailableRecipes
    End Function
End Class
