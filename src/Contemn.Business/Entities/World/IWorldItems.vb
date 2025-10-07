Public Interface IWorldItems
    Function CreateItem(Of TDescriptor)(itemType As String, entity As IInventoryEntity(Of TDescriptor)) As IItem
    Function GetItem(itemId As Integer) As IItem
    Sub ActivateItem(item As IItem)
    Sub DeactivateItem(item As IItem)
    ReadOnly Property ActiveItems As IEnumerable(Of IItem)
End Interface
