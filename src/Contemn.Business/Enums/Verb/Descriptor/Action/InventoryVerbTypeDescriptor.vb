Imports TGGD.Business

Friend Class InventoryVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Friend Sub New()
        MyBase.New(NameOf(InventoryVerbTypeDescriptor), Business.VerbCategoryType.Action, "Inventory")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Return New InventoryDialog(character)
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso character.HasItems
    End Function
End Class
