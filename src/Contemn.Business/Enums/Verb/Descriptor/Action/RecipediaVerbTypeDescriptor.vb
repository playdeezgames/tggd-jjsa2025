Imports TGGD.Business

Friend Class RecipediaVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.VerbType.Recipedia,
            Business.VerbCategoryType.Action,
            """Recipedia""")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New RecipediaDialog(character)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function
End Class
