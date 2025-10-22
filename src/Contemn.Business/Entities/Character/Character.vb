Imports Contemn.Data
Imports TGGD.Business

Friend Class Character
    Inherits InventoryEntity(Of CharacterData, CharacterTypeDescriptor)
    Implements ICharacter

    Public Sub New(
                  data As WorldData,
                  characterId As Integer,
                  platform As IPlatform)
        MyBase.New(
            data,
            platform)
        Me.CharacterId = characterId
    End Sub

    Public ReadOnly Property CharacterType As String Implements ICharacter.CharacterType
        Get
            Return EntityData.CharacterType
        End Get
    End Property

    Public ReadOnly Property CharacterId As Integer Implements ICharacter.CharacterId

    Public ReadOnly Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(Data, EntityData.LocationId, Platform)
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ICharacter.Map
        Get
            Return Location.Map
        End Get
    End Property

    Public ReadOnly Property Column As Integer Implements ICharacter.Column
        Get
            Return Location.Column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ICharacter.Row
        Get
            Return Location.Row
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As CharacterData
        Get
            Return Data.Characters(CharacterId)
        End Get
    End Property

    Public ReadOnly Property AvailableVerbs As IEnumerable(Of String) Implements ICharacter.AvailableVerbs
        Get
            Return VerbTypes.All.Where(Function(x) CanPerform(x))
        End Get
    End Property

    Public Overrides ReadOnly Property Descriptor As CharacterTypeDescriptor
        Get
            Return CharacterTypes.Descriptors(CharacterType)
        End Get
    End Property

    Public ReadOnly Property HasAvailableRecipes As Boolean Implements ICharacter.HasAvailableRecipes
        Get
            Return RecipeTypes.Descriptors.Any(Function(x) x.CanCraft(Me))
        End Get
    End Property

    Public ReadOnly Property IsAvatar As Boolean Implements ICharacter.IsAvatar
        Get
            Return Data.AvatarCharacterId.HasValue AndAlso
                Data.AvatarCharacterId.Value = CharacterId
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return Descriptor.GetName(Me)
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements ICharacter.IsDead
        Get
            Return IsStatisticAtMinimum(StatisticType.Health)
        End Get
    End Property

    Private Function CanPerform(verbType As String) As Boolean
        Return VerbTypes.Descriptors(verbType).CanPerform(Me)
    End Function

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        Data.Locations(EntityData.LocationId).CharacterId = CharacterId
        Descriptor.OnInitialize(Me)
    End Sub

    Public Function Perform(verbType As String) As IDialog Implements ICharacter.Perform
        Return VerbTypes.Descriptors(verbType).Perform(Me)
    End Function

    Public Function MoveTo(destination As ILocation) As IDialog Implements ICharacter.MoveTo
        If destination Is Nothing Then
            Leave()
            Return Nothing
        End If
        If CanEnter(destination) Then
            Return Enter(destination)
        End If
        Return Bump(destination)
    End Function

    Private Function Bump(nextLocation As ILocation) As IDialog
        Me.SetBumpLocation(nextLocation)
        Return Descriptor.OnBump(Me, nextLocation)
    End Function

    Private Function Enter(nextLocation As ILocation) As IDialog
        Leave()
        EntityData.LocationId = nextLocation.LocationId
        Data.Locations(EntityData.LocationId).CharacterId = CharacterId
        Descriptor.OnEnter(Me, Location)
        Return Location.HandleEnter(Me)
    End Function

    Private Sub Leave()
        SetBumpLocation(Nothing)
        Descriptor.OnLeave(Me, Location)
        Location.HandleLeave(Me)
        Data.Locations(EntityData.LocationId).CharacterId = Nothing
    End Sub

    Private Function CanEnter(nextLocation As ILocation) As Boolean
        Return Not nextLocation.HasCharacter AndAlso nextLocation.Descriptor.CanEnter(nextLocation, Me)
    End Function

    Public Overrides Sub Recycle()
        World.DeactivateCharacter(Me)
        Clear()
        Data.RecycledCharacters.Add(CharacterId)
    End Sub

    Protected Overrides Sub HandleAddItem(item As IItem)
        Descriptor.HandleAddItem(Me, item)
    End Sub

    Protected Overrides Sub HandleRemoveItem(item As IItem)
        Descriptor.HandleRemoveItem(Me, item)
    End Sub

    Public Function AvailableVerbsOfCategory(verbCategoryType As String) As IEnumerable(Of String) Implements ICharacter.AvailableVerbsOfCategory
        Return VerbTypes.AllOfCategory(verbCategoryType).Where(Function(x) CanPerform(x))
    End Function

    Public Function ProcessTurn() As IEnumerable(Of IDialogLine) Implements ICharacter.ProcessTurn
        Return Descriptor.OnProcessTurn(Me)
    End Function

    Public Overrides Sub Clear()
        MyBase.Clear()
        EntityData.LocationId = 0
        EntityData.CharacterType = Nothing
    End Sub

    Public Sub Dismantle(item As IItem) Implements ICharacter.Dismantle
        Dim descriptor = item.Descriptor
        RemoveAndRecycleItem(item)
        For Each entry In descriptor.DepletionTable
            For Each dummy In Enumerable.Range(0, entry.Value)
                World.CreateItem(entry.Key, Me)
            Next
        Next
    End Sub

    Public Sub SetBumpLocation(location As ILocation) Implements ICharacter.SetBumpLocation
        SetStatistic(StatisticType.BumpLocationId, location?.LocationId)
    End Sub

    Public Function GetBumpLocation() As ILocation Implements ICharacter.GetBumpLocation
        If Not HasStatistic(StatisticType.BumpLocationId) Then
            Return Nothing
        End If
        Return World.GetLocation(GetStatistic(StatisticType.BumpLocationId))
    End Function

    Public Function CraftRecipe(recipeType As String, nextDialog As Func(Of IDialog), confirmed As Boolean) As IDialog Implements ICharacter.CraftRecipe
        Dim descriptor = RecipeTypes.Descriptors.Single(Function(x) x.RecipeType = recipeType)
        Dim character = Me
        If Descriptor.IsDestructive AndAlso Not confirmed Then
            Return New ConfirmDialog(
                "Are you sure?",
                {
                    New DialogLine(MoodType.Warning, "This recipe is destructive."),
                    New DialogLine(MoodType.Info, "Please confirm.")
                },
                Function() Character.CraftRecipe(recipeType, nextDialog, True),
                nextDialog)
        Else
            Dim CRAFT_ANOTHER_CHOICE As String = NameOf(CRAFT_ANOTHER_CHOICE)
            Const CRAFT_ANOTHER_TEXT = "Craft Another"
            Dim messageLines = Descriptor.Craft(Character)
            Character.Platform.PlaySfx(Sfx.Craft)
            Character.ChangeStatistic(StatisticType.Score, 1)
            Return New MessageDialog(
            "Behold!",
            messageLines,
            {
                (OK_CHOICE, OK_TEXT, nextDialog, True),
                (CRAFT_ANOTHER_CHOICE, CRAFT_ANOTHER_TEXT, Function() character.CraftRecipe(recipeType, nextDialog, True), descriptor.CanCraft(character))
            },
            nextDialog)
        End If
    End Function
End Class
