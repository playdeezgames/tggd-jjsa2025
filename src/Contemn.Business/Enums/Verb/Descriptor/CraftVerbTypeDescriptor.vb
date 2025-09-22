Friend Class CraftVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.Craft, Business.VerbCategoryType.Action, "Craft...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As TGGD.Business.IDialog
        Return New CraftDialog(character)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso RecipeTypes.Descriptors.Any(Function(x) x.Value.CanCraft(character))
    End Function
End Class
