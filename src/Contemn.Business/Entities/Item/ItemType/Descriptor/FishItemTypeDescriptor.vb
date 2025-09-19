Imports TGGD.Business

Friend Class FishItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.ItemType.Fish,
            "Fish",
            0,
            True)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return ItemTypeName
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case EAT_CHOICE
                Return Eat(item, character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Eat(item As IItem, character As ICharacter) As IDialog
        Return New MessageDialog(
            {},
            {(NEVER_MIND_CHOICE, NEVER_MIND_TEXT, ItemTypeDialog.LaunchMenu(character, ItemType), True)},
            ItemTypeDialog.LaunchMenu(character, ItemType))
    End Function

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Return {(EAT_CHOICE, EAT_TEXT)}
    End Function
End Class
