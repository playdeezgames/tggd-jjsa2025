Public Interface IWorldItems
    Function CreateItem(Of TDescriptor)(itemType As String, entity As IInventoryEntity(Of TDescriptor)) As IItem
    Function GetItem(itemId As Integer) As IItem
End Interface
