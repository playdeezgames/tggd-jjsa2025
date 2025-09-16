Imports Contemn.Data

Friend Class Location
    Inherits InventoryEntity(Of LocationData)
    Implements ILocation

    Public Sub New(data As WorldData, locationId As Integer, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
        Me.LocationId = locationId
    End Sub

    Public ReadOnly Property LocationId As Integer Implements ILocation.LocationId

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        LocationType.ToLocationTypeDescriptor.OnInitialize(Me)
    End Sub

    Public Property LocationType As String Implements ILocation.LocationType
        Get
            Return EntityData.LocationType
        End Get
        Set(value As String)
            If value <> EntityData.LocationType Then
                EntityData.LocationType = value
                Initialize()
            End If
        End Set
    End Property

    Public ReadOnly Property Column As Integer Implements ILocation.Column
        Get
            Return EntityData.Column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ILocation.Row
        Get
            Return EntityData.Row
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ILocation.Map
        Get
            Return New Map(Data, EntityData.MapId, AddressOf PlaySfx)
        End Get
    End Property

    Public ReadOnly Property HasCharacter As Boolean Implements ILocation.HasCharacter
        Get
            Return EntityData.CharacterId.HasValue
        End Get
    End Property

    Public ReadOnly Property Character As ICharacter Implements ILocation.Character
        Get
            Return If(HasCharacter, New Character(Data, EntityData.CharacterId.Value, AddressOf PlaySfx), Nothing)
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As LocationData
        Get
            Return Data.Locations(LocationId)
        End Get
    End Property

    Public Overrides Sub Recycle()
        Clear()
        Data.RecycledLocations.Add(LocationId)
    End Sub

    Protected Overrides Sub HandleAddItem(item As IItem)
    End Sub

    Protected Overrides Sub HandleRemoveItem(item As IItem)
    End Sub
End Class
