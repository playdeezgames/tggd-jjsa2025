Imports TGGD.Business

Friend Class GroundVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(GroundVerbTypeDescriptor),
            Business.VerbCategoryType.Action,
            "Ground...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Return GroundDialog.LaunchMenu(character).Invoke()
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso
            character.Location.HasItems
    End Function
End Class
