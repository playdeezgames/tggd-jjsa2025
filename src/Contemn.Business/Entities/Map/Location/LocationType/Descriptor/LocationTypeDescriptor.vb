Imports TGGD.Business

Friend MustInherit Class LocationTypeDescriptor
    Friend ReadOnly Property LocationType As String
    Sub New(locationType As String)
        Me.LocationType = locationType
    End Sub

    Friend MustOverride Function OnBump(location As ILocation, character As ICharacter) As IDialog
    Friend MustOverride Sub OnLeave(location As ILocation, character As ICharacter)
    Friend MustOverride Function OnEnter(location As ILocation, character As ICharacter) As IDialog
    Friend MustOverride Function CanEnter(location As ILocation, character As ICharacter) As Boolean
    Friend MustOverride Function CanSpawn(location As ILocation, itemType As String) As Boolean
    Friend MustOverride Sub OnInitialize(location As Location)
End Class
