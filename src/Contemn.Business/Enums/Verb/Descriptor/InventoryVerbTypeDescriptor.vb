Imports TGGD.Business

Friend Class InventoryVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.Inventory, Business.VerbCategoryType.Action, "Inventory")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New InventoryDialog(character)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso character.HasItems
    End Function
End Class
