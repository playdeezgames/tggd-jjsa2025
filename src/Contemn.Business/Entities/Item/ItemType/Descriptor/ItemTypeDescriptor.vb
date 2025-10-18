Imports TGGD.Business

Public MustInherit Class ItemTypeDescriptor
    Friend ReadOnly Property ItemType As String
    Friend ReadOnly Property ItemTypeName As String
    Friend ReadOnly Property ItemCount As Integer
    Friend ReadOnly Property IsAggregate As Boolean
    Friend ReadOnly AutoTags As IEnumerable(Of String)
    Friend ReadOnly DepletionTable As IReadOnlyDictionary(Of String, Integer)
    Sub New(
           itemType As String,
           itemTypeName As String,
           itemCount As Integer,
           isAggregate As Boolean,
           autoTags As IEnumerable(Of String),
           depletionTable As IReadOnlyDictionary(Of String, Integer))
        Me.ItemType = itemType
        Me.ItemTypeName = itemTypeName
        Me.ItemCount = itemCount
        Me.IsAggregate = isAggregate
        Me.AutoTags = autoTags
    End Sub
    Friend MustOverride Function CanSpawnMap(map As IMap) As Boolean
    Friend MustOverride Function CanSpawnLocation(location As ILocation) As Boolean
    Friend MustOverride Function GetName(item As IItem) As String
    Friend MustOverride Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
    Friend MustOverride Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
    Friend MustOverride Sub HandleAddItem(item As IItem, character As ICharacter)
    Friend MustOverride Sub HandleRemoveItem(item As IItem, character As ICharacter)
    Friend Overridable Sub HandleInitialize(item As IItem)
        For Each tag In AutoTags
            item.SetTag(tag, True)
        Next
    End Sub
    Friend MustOverride Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
    Friend Overridable Function OnProcessTurn(item As IItem) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function
End Class
