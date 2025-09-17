Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.Forage, "Forage...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return Nothing
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.GetTag(TagType.CanForage)
    End Function
End Class
