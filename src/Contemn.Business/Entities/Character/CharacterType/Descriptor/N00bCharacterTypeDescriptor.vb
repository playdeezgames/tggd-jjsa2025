Imports TGGD.Business

Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(Business.CharacterType.N00b, 1)
    End Sub

    Const MAXIMUM_SATIETY = 100
    Const MAXIMUM_HEALTH = 100
    Const MAXIMUM_HYDRATION = 100
    Const SATIETY_WARNING = MAXIMUM_SATIETY / 10
    Const HYDRATION_WARNING = MAXIMUM_HYDRATION / 10

    Friend Overrides Sub OnInitialize(character As ICharacter)
        character.World.Avatar = character

        character.SetStatisticRange(StatisticType.Health, MAXIMUM_HEALTH, 0, MAXIMUM_HEALTH)
        character.SetStatisticRange(StatisticType.Satiety, MAXIMUM_SATIETY, 0, MAXIMUM_SATIETY)
        character.SetStatisticRange(StatisticType.Hydration, MAXIMUM_HYDRATION, 0, MAXIMUM_HYDRATION)
        character.SetStatisticRange(StatisticType.Illness, 0, 0, Integer.MaxValue)
    End Sub

    Friend Overrides Function OnBump(character As ICharacter, location As ILocation) As IDialog
        Return location.HandleBump(character)
    End Function

    Friend Overrides Sub OnEnter(character As ICharacter, location As ILocation)
        For Each line In character.ProcessTurn()
            character.World.AddMessage(line.Mood, line.Text)
        Next
        Dim items = location.Items
        For Each item In items
            location.RemoveItem(item)
            character.World.AddMessage(MoodType.Info, $"You pick up {item.Name}.")
            character.AddItem(item)
        Next
    End Sub

    Friend Overrides Function OnProcessTurn(character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        Dim result As New List(Of (Mood As String, Text As String))
        result.AddRange(ProcessIllness(character))
        result.AddRange(ProcessStarvation(character))
        result.AddRange(ProcessHunger(character))
        result.AddRange(ProcessDehydration(character))
        Return result
    End Function

    Private Function ProcessIllness(character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        Dim result As New List(Of (Mood As String, Text As String))
        If Not character.IsStatisticAtMinimum(StatisticType.Illness) Then
            Dim illness = character.GetStatistic(StatisticType.Illness)
            result.Add((MoodType.Danger, $"-{illness} HLT due to illness."))
            character.PlaySfx(Sfx.PlayerHit)
            character.ChangeStatistic(StatisticType.Health, -illness)
            character.ChangeStatistic(StatisticType.Illness, -1)
        End If
        Return result
    End Function

    Private Function ProcessDehydration(character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        Dim result As New List(Of (Mood As String, Text As String))
        If character.IsStatisticAtMinimum(StatisticType.Hydration) Then
            result.Add((MoodType.Danger, "Yer dehydrated! Drink immediately!"))
        Else
            If character.ChangeStatistic(StatisticType.Hydration, -1) < HYDRATION_WARNING Then
                result.Add((MoodType.Warning, "Yer thirsty! Drink soon!"))
            End If
        End If
        Return result
    End Function

    Private Function ProcessHunger(character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        Dim result As New List(Of (Mood As String, Text As String))
        If character.IsStatisticAtMinimum(StatisticType.Satiety) Then
            result.Add((MoodType.Danger, "Yer starving! Eat immediately!"))
        Else
            If character.ChangeStatistic(StatisticType.Satiety, -1) < SATIETY_WARNING Then
                result.Add((MoodType.Warning, "Yer famished! Eat soon!"))
            End If
        End If
        Return result
    End Function

    Private Function ProcessStarvation(character As ICharacter) As IEnumerable(Of (Mood As String, Text As String))
        If character.IsStatisticAtMinimum(StatisticType.Satiety) OrElse character.IsStatisticAtMinimum(StatisticType.Hydration) Then
            character.PlaySfx(Sfx.PlayerHit)
            character.ChangeStatistic(StatisticType.Health, -1)
        End If
        If character.IsDead Then
            Return {(MoodType.Danger, "Yer dead.")}
        End If
        Return Array.Empty(Of (Mood As String, Text As String))
    End Function

    Friend Overrides Sub OnLeave(character As ICharacter, location As ILocation)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return True
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return Not location.HasCharacter AndAlso location.LocationType = LocationType.Grass
    End Function

    Friend Overrides Sub HandleAddItem(character As ICharacter, item As IItem)
        item.ItemType.ToItemTypeDescriptor.HandleAddItem(item, character)
    End Sub

    Friend Overrides Sub HandleRemoveItem(character As ICharacter, item As IItem)
        item.ItemType.ToItemTypeDescriptor.HandleRemoveItem(item, character)
    End Sub

    Friend Overrides Function OnInteract(target As ICharacter, initiator As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function
End Class
