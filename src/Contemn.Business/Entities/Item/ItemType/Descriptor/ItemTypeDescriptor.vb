Imports TGGD.Business

Friend MustInherit Class ItemTypeDescriptor
    ReadOnly Property ItemType As String
    ReadOnly Property ItemTypeName As String
    ReadOnly Property ItemCount As Integer
    ReadOnly Property IsAggregate As Boolean
    Sub New(
           itemType As String,
           itemTypeName As String,
           itemCount As Integer,
           isAggregate As Boolean)
        Me.ItemType = itemType
        Me.ItemTypeName = itemTypeName
        Me.ItemCount = itemCount
        Me.IsAggregate = isAggregate
    End Sub
    Friend MustOverride Function CanSpawnMap(map As IMap) As Boolean
    Friend MustOverride Function CanSpawnLocation(location As ILocation) As Boolean
    Friend MustOverride Function GetName(item As Item) As String
    Friend MustOverride Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
    Friend MustOverride Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
    Friend MustOverride Sub HandleAddItem(item As IItem, character As ICharacter)
    Friend MustOverride Sub HandleRemoveItem(item As IItem, character As ICharacter)
    Friend MustOverride Sub HandleInitialize(item As Item)
End Class
