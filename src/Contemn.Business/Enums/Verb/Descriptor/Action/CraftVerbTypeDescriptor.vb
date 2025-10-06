Friend Class CraftVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(CraftVerbTypeDescriptor), Business.VerbCategoryType.Action, "Craft...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As TGGD.Business.IDialog
        Return New CraftDialog(character)
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso RecipeTypes.Descriptors.Any(Function(x) x.Value.CanCraft(character))
    End Function
End Class
