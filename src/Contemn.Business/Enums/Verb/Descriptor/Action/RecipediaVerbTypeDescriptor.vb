Imports TGGD.Business

Friend Class RecipediaVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(RecipediaVerbTypeDescriptor),
            Business.VerbCategoryType.Action,
            """Recipedia""")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Return New RecipediaDialog(character)
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function
End Class
