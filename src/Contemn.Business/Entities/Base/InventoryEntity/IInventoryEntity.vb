Public Interface IInventoryEntity(Of TDescriptor)
    Inherits IDescribedEntity(Of TDescriptor)
    Sub AddItem(item As IItem)
    Sub RemoveItem(item As IItem)
    Function HasItem(item As IItem) As Boolean

    ReadOnly Property HasItems As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)

    Function GetCountOfItemType(itemType As String) As Integer
    Function GetItemOfType(itemType As String) As IItem
    Function HasItemsOfType(itemType As String) As Boolean
    Function ItemsOfType(itemType As String) As IEnumerable(Of IItem)
End Interface
