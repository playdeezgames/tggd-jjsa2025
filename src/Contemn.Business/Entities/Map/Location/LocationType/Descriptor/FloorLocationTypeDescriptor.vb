Imports TGGD.Business

Friend Class FloorLocationTypeDescriptor
    Inherits LocationTypeDescriptor
    Public Sub New()
        MyBase.New(Business.LocationType.Floor)
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return location.Character.Interact(character)
    End Function

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
    End Sub

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Return Nothing
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return True
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return True
    End Function

    Friend Overrides Sub OnInitialize(location As Location)
    End Sub
End Class
