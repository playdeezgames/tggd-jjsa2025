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

    Public ReadOnly Property ActiveLocations As IEnumerable(Of ILocation) Implements IWorld.ActiveLocations
        Get
            Return EntityData.ActiveLocations.Select(AddressOf GetLocation)
        End Get
    End Property

    Public ReadOnly Property ActiveCharacters As IEnumerable(Of ICharacter) Implements IWorld.ActiveCharacters
        Get
            Return EntityData.ActiveCharacters.Select(AddressOf GetCharacter)
        End Get
    End Property

    Public ReadOnly Property ActiveItems As IEnumerable(Of IItem) Implements IWorldItems.ActiveItems
        Get
            Return EntityData.ActiveItems.Select(AddressOf GetItem)
        End Get
    End Property

    Public Overrides Sub Clear()
        MyBase.Clear()
        Data.Maps.Clear()
        Data.RecycledMaps.Clear()

        Data.Locations.Clear()
        Data.RecycledLocations.Clear()
        Data.ActiveLocations.Clear()

        Data.Characters.Clear()
        Data.RecycledCharacters.Clear()
        Data.AvatarCharacterId = Nothing
        Data.ActiveCharacters.Clear()

        Data.Messages.Clear()

        Data.Items.Clear()
        Data.RecycledItems.Clear()

        Data.Generators.Clear()
        Data.RecycledGenerators.Clear()
    End Sub
    Public Overrides Sub Initialize()
        MyBase.Initialize()
        DoInitialization()
    End Sub

    Private Sub DoInitialization()
        CreateMaps()
        CreateCharacters()
        CreateItems()
        If GetMetadata(MetadataType.Difficulty) = TUTORIAL_DIFFICULTY Then
            InitializeTutorial()
        End If
        AddMessage(MoodType.Info, "Welcome to Grubstaker of SPLORR!!")
        AddMessage(MoodType.Info, "MOVE: Arrows, WASD, ZQSD")
        AddMessage(MoodType.Info, "ACTION MENU: Space")
        AddMessage(MoodType.Info, "GAME MENU: Backspace")
    End Sub

    Private Sub InitializeTutorial()
        Dim avatarLocation = Avatar.Location
        Dim avatarMap = avatarLocation.Map
        Dim location = RNG.FromEnumerable(DirectionTypes.All.Select(
            Function(x)
                Dim descriptor = DirectionTypes.Descriptors(x)
                Return avatarMap.GetLocation(
                    descriptor.GetNextColumn(avatarLocation.Column),
                    descriptor.GetNextRow(avatarLocation.Row))
            End Function).Where(Function(x) x IsNot Nothing))
        location.LocationType = NameOf(TutorialHouseLocationTypeDescriptor)
    End Sub

    Private Sub CreateItems()
        For Each descriptor In ItemTypes.Descriptors.Values
            Dim candidateMaps = Maps.Where(Function(x) descriptor.CanSpawnMap(x))
            For Each dummy In Enumerable.Range(0, descriptor.ItemCount)
                Dim map = RNG.FromEnumerable(candidateMaps)
                Dim candidateLocations = map.Locations.Where(Function(x) descriptor.CanSpawnLocation(x))
                CreateItem(descriptor.ItemType, RNG.FromEnumerable(candidateLocations))
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
        For Each descriptor In CharacterTypes.Descriptors.Values
            Dim candidateMaps = Maps.Where(Function(x) descriptor.CanSpawnMap(x))
            For Each dummy In Enumerable.Range(0, descriptor.CharacterCount)
                Dim map = RNG.FromEnumerable(candidateMaps)
                Dim candidateLocations = map.Locations.Where(Function(x) descriptor.CanSpawnLocation(x))
                CreateCharacter(descriptor.CharacterType, RNG.FromEnumerable(candidateLocations))
            Next
        Next
    End Sub

    Private Sub CreateMaps()
        For Each descriptor In MapTypes.Descriptors.Values
            For Each dummy In Enumerable.Range(0, descriptor.MapCount)
                CreateMap(descriptor.MapType)
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

    Public Function CreateItem(Of TDescriptor)(itemType As String, entity As IInventoryEntity(Of TDescriptor)) As IItem Implements IWorld.CreateItem
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

    Public Function GetGenerator(generatorId As Integer) As IGenerator Implements IWorld.GetGenerator
        Return New Generator(Data, generatorId)
    End Function

    Public Function CreateGenerator(generatorType As String) As IGenerator Implements IWorld.CreateGenerator
        Dim generatorId As Integer
        If Data.RecycledGenerators.Any Then
            generatorId = Data.RecycledGenerators.First
            Data.Generators(generatorId).Clear()
            Data.RecycledGenerators.Remove(generatorId)
        Else
            generatorId = Data.Generators.Count
            Data.Generators.Add(New Dictionary(Of String, Integer)())
        End If
        Dim result = New Generator(Data, generatorId)
        generatorType.ToGeneratorTypeDescriptor.Initialize(result)
        Return result
    End Function

    Public Function GetCharacter(characterId As Integer) As ICharacter Implements IWorld.GetCharacter
        Return New Character(Data, characterId, AddressOf PlaySfx)
    End Function

    Public Function ProcessTurn() As IEnumerable(Of IDialogLine) Implements IWorld.ProcessTurn
        For Each location In ActiveLocations
            location.ProcessTurn()
        Next
        Dim result As New List(Of IDialogLine)
        For Each item In ActiveItems
            Dim lines = item.ProcessTurn()
            If Avatar.HasItem(item) Then
                result.AddRange(lines)
            End If
        Next
        For Each character In ActiveCharacters
            Dim lines = character.ProcessTurn()
            If character.IsAvatar Then
                result.AddRange(lines)
            End If
        Next
        Return result
    End Function

    Public Sub ActivateLocation(location As ILocation) Implements IWorld.ActivateLocation
        EntityData.ActiveLocations.Add(location.LocationId)
    End Sub

    Public Sub DeactivateLocation(location As ILocation) Implements IWorld.DeactivateLocation
        EntityData.ActiveLocations.Remove(location.LocationId)
    End Sub

    Public Sub ActivateCharacter(character As ICharacter) Implements IWorld.ActivateCharacter
        EntityData.ActiveCharacters.Add(character.CharacterId)
    End Sub

    Public Sub DeactivateCharacter(character As ICharacter) Implements IWorld.DeactivateCharacter
        EntityData.ActiveCharacters.Remove(character.CharacterId)
    End Sub

    Public Sub ActivateItem(item As IItem) Implements IWorldItems.ActivateItem
        EntityData.ActiveItems.Add(item.ItemId)
    End Sub

    Public Sub DeactivateItem(item As IItem) Implements IWorldItems.DeactivateItem
        EntityData.ActiveItems.Remove(item.ItemId)
    End Sub

    Public Sub PrepareAndInitialize(preparation As Action(Of IWorld)) Implements IWorld.PrepareAndInitialize
        MyBase.Initialize()
        preparation(Me)
        DoInitialization()
    End Sub
End Class
