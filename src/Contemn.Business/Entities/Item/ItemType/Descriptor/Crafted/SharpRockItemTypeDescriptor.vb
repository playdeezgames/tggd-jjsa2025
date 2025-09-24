Imports TGGD.Business

Friend Class SharpRockItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(SharpRockItemTypeDescriptor), "Sharp Rock", 0, True)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As IItem) As String
        Return Me.ItemTypeName
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Return Array.Empty(Of IDialogChoice)
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a sharp rock.")
        }
    End Function
End Class
