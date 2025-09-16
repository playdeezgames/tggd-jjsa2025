Imports TGGD.Business

Friend Class WallLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Wall)
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return Nothing
    End Function

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return False
    End Function

    Friend Overrides Sub OnInitialize(location As Location)
    End Sub
End Class
