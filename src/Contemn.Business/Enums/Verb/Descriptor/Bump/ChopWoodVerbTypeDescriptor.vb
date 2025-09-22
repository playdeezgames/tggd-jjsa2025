Imports TGGD.Business

Friend Class ChopWoodVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Const RESOURCE_PER_LOG = 5

    Public Sub New()
        MyBase.New(
            Business.VerbType.ChopWood,
            Business.VerbCategoryType.Bump,
            "Chop Wood")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso
            character.GetBumpLocation().GetTag(TagType.IsChoppable) AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanChop))
    End Function
End Class
