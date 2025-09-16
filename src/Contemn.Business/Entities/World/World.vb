Imports System.Data
Imports TGGD.Business
Imports Contemn.Data

Public Class World
    Inherits Entity(Of WorldData)
    Implements IWorld
    Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub

    Public ReadOnly Property Maps As IEnumerable(Of IMap) Implements IWorld.Maps
        Get
            Return Enumerable.
                Range(0, Data.Maps.Count).
                Select(Function(x) New Business.Map(Data, x, AddressOf PlaySfx))
        End Get
    End Property

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            Return If(
                Data.AvatarCharacterId.HasValue,
                New Character(Data, Data.AvatarCharacterId.Value, AddressOf PlaySfx),
                Nothing)
        End Get
        Set(value As ICharacter)
            Data.AvatarCharacterId = value?.CharacterId
        End Set
    End Property

    Public ReadOnly Property MessageCount As Integer Implements IWorld.MessageCount
        Get
            Return Data.Messages.Count
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As WorldData
        Get
            Return Data
        End Get
    End Property
    Public Overrides Sub Clear()
        MyBase.Clear()
        Data.Maps.Clear()
        Data.RecycledMaps.Clear()
        Data.Locations.Clear()
        Data.RecycledLocations.Clear()
        Data.Characters.Clear()
        Data.RecycledCharacters.Clear()
        Data.Messages.Clear()
        Data.Items.Clear()
        Data.RecycledItems.Clear()
        Data.AvatarCharacterId = Nothing
    End Sub
    Public Overrides Sub Initialize()
        MyBase.Initialize()
        CreateMaps()
        CreateCharacters()
        CreateItems()
        AddMessage(MoodType.Info, "Welcome to PLACEHOLDER")
        AddMessage(MoodType.Info, "Arrows, WASD, ZQSD: MOVE")
        AddMessage(MoodType.Info, "Space: ACTIONS | Backspace: GAME MENU")
    End Sub

    Private Sub CreateItems()
        For Each itemType In ItemTypes.All
            Dim descriptor = itemType.ToItemTypeDescriptor
            Dim candidateMaps = Maps.Where(Function(x) descriptor.CanSpawnMap(x))
            For Each dummy In Enumerable.Range(0, descriptor.itemCount)
                Dim map = RNG.FromEnumerable(candidateMaps)
                Dim candidateLocations = map.Locations.Where(Function(x) descriptor.CanSpawnLocation(x))
                CreateItem(itemType, RNG.FromEnumerable(candidateLocations))
            Next
        Next
    End Sub

    Public Sub AddMessage(mood As String, text As String) Implements IWorld.AddMessage
        Data.Messages.Add(New MessageData With {.Mood = mood, .Text = text})
    End Sub

    Public Sub DismissMessage() Implements IWorld.DismissMessage
        If Data.Messages.Any Then
            Data.Messages.RemoveAt(0)
        End If
    End Sub

    Private Sub CreateCharacters()
        For Each characterType In CharacterTypes.All
            Dim descriptor = characterType.ToCharacterTypeDescriptor
            Dim candidateMaps = Maps.Where(Function(x) descriptor.CanSpawnMap(x))
            For Each dummy In Enumerable.Range(0, descriptor.CharacterCount)
                Dim map = RNG.FromEnumerable(candidateMaps)
                Dim candidateLocations = map.Locations.Where(Function(x) descriptor.CanSpawnLocation(x))
                CreateCharacter(characterType, RNG.FromEnumerable(candidateLocations))
            Next
        Next
    End Sub

    Private Sub CreateMaps()
        For Each mapType In MapTypes.All
            Dim descriptor = mapType.ToMapTypeDescriptor
            For Each dummy In Enumerable.Range(0, descriptor.MapCount)
                CreateMap(mapType)
            Next
        Next
    End Sub

    Public Function CreateMap(mapType As String) As IMap Implements IWorld.CreateMap
        Dim mapId = Data.Maps.Count
        Data.Maps.Add(New MapData With {.MapType = mapType})
        Dim result = New Map(Data, mapId, AddressOf PlaySfx)
        result.Initialize()
        Return result
    End Function

    Public Function CreateLocation(locationType As String, map As IMap, column As Integer, row As Integer) As ILocation Implements IWorld.CreateLocation
        Dim locationId = Data.Locations.Count
        Data.Locations.Add(New LocationData With {
                            .LocationType = locationType,
                            .MapId = map.MapId,
                            .Column = column,
                            .Row = row})
        Dim result = New Location(
            Data,
            locationId,
            AddressOf PlaySfx)
        map.SetLocation(column, row, result)
        result.Initialize()
        Return result
    End Function

    Public Function CreateCharacter(characterType As String, location As ILocation) As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = Data.Characters.Count
        Data.Characters.Add(New CharacterData With {
                            .CharacterType = characterType,
                            .LocationId = location.LocationId})
        Dim result = New Character(
            Data,
            characterId,
            AddressOf PlaySfx)
        result.Initialize()
        Return result
    End Function

    Public Function CreateItem(itemType As String, entity As IInventoryEntity) As IItem Implements IWorld.CreateItem
        Dim itemId As Integer
        If Data.RecycledItems.Any Then
            itemId = Data.RecycledItems.First
            Data.RecycledItems.Remove(itemId)
            Data.Items(itemId) = New ItemData With {.ItemType = itemType}
        Else
            itemId = Data.Items.Count
            Data.Items.Add(New ItemData With {.ItemType = itemType})
        End If
        Dim result = New Item(Data, itemId, AddressOf PlaySfx)
        result.Initialize()
        entity.AddItem(result)
        Return result
    End Function

    Public Function GetMap(mapId As Integer) As IMap Implements IWorld.GetMap
        Return New Map(Data, mapId, AddressOf PlaySfx)
    End Function

    Public Function GetLocation(locationId As Integer) As ILocation Implements IWorld.GetLocation
        Return New Location(Data, locationId, AddressOf PlaySfx)
    End Function

    Public Function GetMessage(line As Integer) As IMessage Implements IWorld.GetMessage
        Return New Message(Data, line)
    End Function

    Public Function GetItem(itemId As Integer) As IItem Implements IWorld.GetItem
        Return New Item(Data, itemId, AddressOf PlaySfx)
    End Function

    Public Overrides Sub Recycle()
        Clear()
    End Sub
End Class
