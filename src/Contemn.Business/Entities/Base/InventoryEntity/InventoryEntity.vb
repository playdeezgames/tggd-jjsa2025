Imports Contemn.Data

Friend MustInherit Class InventoryEntity(Of TEntityData As InventoryEntityData, TDescriptor)
    Inherits DescribedEntity(Of TEntityData, TDescriptor)
    Implements IInventoryEntity(Of TDescriptor)

    Protected Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub

    Public ReadOnly Property HasItems As Boolean Implements IInventoryEntity(Of TDescriptor).HasItems
        Get
            Return EntityData.ItemIds.Any
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IInventoryEntity(Of TDescriptor).Items
        Get
            Return EntityData.ItemIds.Select(Function(x) World.GetItem(x))
        End Get
    End Property

    Public Sub AddItem(item As IItem) Implements IInventoryEntity(Of TDescriptor).AddItem
        EntityData.ItemIds.Add(item.ItemId)
        HandleAddItem(item)
    End Sub

    Protected MustOverride Sub HandleAddItem(item As IItem)

    Public Sub RemoveItem(item As IItem) Implements IInventoryEntity(Of TDescriptor).RemoveItem
        HandleRemoveItem(item)
        EntityData.ItemIds.Remove(item.ItemId)
    End Sub

    Protected MustOverride Sub HandleRemoveItem(item As IItem)

    Public Function GetCountOfItemType(itemType As String) As Integer Implements IInventoryEntity(Of TDescriptor).GetCountOfItemType
        Return Items.Count(Function(x) x.ItemType = itemType)
    End Function

    Public Function GetItemOfType(itemType As String) As IItem Implements IInventoryEntity(Of TDescriptor).GetItemOfType
        Return Items.First(Function(x) x.ItemType = itemType)
    End Function

    Public Function HasItemsOfType(itemType As String) As Boolean Implements IInventoryEntity(Of TDescriptor).HasItemsOfType
        Return Items.Any(Function(x) x.ItemType = itemType)
    End Function

    Public Function ItemsOfType(itemType As String) As IEnumerable(Of IItem) Implements IInventoryEntity(Of TDescriptor).ItemsOfType
        Return Items.Where(Function(x) x.ItemType = itemType)
    End Function

    Public Overrides Sub Clear()
        MyBase.Clear()
        EntityData.ItemIds.Clear()
    End Sub

    Public Function HasItem(item As IItem) As Boolean Implements IInventoryEntity(Of TDescriptor).HasItem
        Return EntityData.ItemIds.Contains(item.ItemId)
    End Function
End Class
