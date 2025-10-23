Imports TGGD.Business

public Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(NameOf(N00bCharacterTypeDescriptor), "N00b", 1)
    End Sub

    Const MAXIMUM_SATIETY = 100
    Const MAXIMUM_HEALTH = 100
    Const MAXIMUM_HYDRATION = 100
    Const SATIETY_WARNING = MAXIMUM_SATIETY / 10
    Const HYDRATION_WARNING = MAXIMUM_HYDRATION / 10
    Const MAXIMUM_RECOVERY = 10
    Const VIEW_INRADIUS = 1

    Friend Overrides Sub OnInitialize(character As ICharacter)
        character.World.Avatar = character
        character.World.ActivateCharacter(character)

        character.SetStatisticRange(StatisticType.Health, MAXIMUM_HEALTH, 0, MAXIMUM_HEALTH)
        character.SetStatisticRange(StatisticType.Satiety, MAXIMUM_SATIETY, 0, MAXIMUM_SATIETY)
        character.SetStatisticRange(StatisticType.Hydration, MAXIMUM_HYDRATION, 0, MAXIMUM_HYDRATION)
        character.SetStatisticRange(StatisticType.Illness, 0, 0, Integer.MaxValue)
        character.SetStatisticRange(StatisticType.Score, 0, 0, Integer.MaxValue)
        character.SetStatisticRange(StatisticType.Recovery, 0, 0, MAXIMUM_RECOVERY)
        character.SetStatisticRange(StatisticType.FishSlapCounter, 0, 0, Integer.MaxValue)

        UpdateFogOfWar(character)
    End Sub

    Private Shared Sub UpdateFogOfWar(character As ICharacter)
        For Each column In Enumerable.Range(character.Column - VIEW_INRADIUS, VIEW_INRADIUS * 2 + 1)
            For Each row In Enumerable.Range(character.Row - VIEW_INRADIUS, VIEW_INRADIUS * 2 + 1)
                Dim location = character.Map.GetLocation(column, row)
                location?.SetTag(TagType.Visible, True)
            Next
        Next
    End Sub

    Friend Overrides Function OnBump(character As ICharacter, location As ILocation) As IDialog
        Return location.Descriptor.OnBump(location, character)
    End Function

    Friend Overrides Sub OnEnter(character As ICharacter, location As ILocation)
        For Each line In character.World.ProcessTurn()
            character.World.AddMessage(line.Mood, line.Text)
        Next
        UpdateFogOfWar(character)
    End Sub

    Friend Overrides Function OnProcessTurn(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim result As New List(Of IDialogLine)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.SetTag(TagType.CanRecover, Not character.IsStatisticAtMaximum(StatisticType.Health))
        result.AddRange(ProcessIllness(character))
        result.AddRange(ProcessStarvation(character))
        If Not character.IsDead Then
            result.AddRange(ProcessHunger(character))
            result.AddRange(ProcessDehydration(character))
        End If
        result.AddRange(ProcessRecovery(character))
        Return result
    End Function

    Private Shared Function ProcessRecovery(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim result As New List(Of IDialogLine)
        If Not character.GetTag(TagType.CanRecover) Then
            character.SetStatistic(StatisticType.Recovery, 0)
        ElseIf character.IsStatisticAtMaximum(StatisticType.Recovery) Then
            character.SetStatistic(StatisticType.Recovery, 0)
            character.ChangeStatistic(StatisticType.Health, 1)
            result.Add(New DialogLine(MoodType.Info, $"+1 {character.FormatStatistic(StatisticType.Health)}"))
        Else
            character.ChangeStatistic(StatisticType.Recovery, 1)
        End If
        Return result
    End Function

    Private Shared Function ProcessIllness(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim result As New List(Of IDialogLine)
        If Not character.IsStatisticAtMinimum(StatisticType.Illness) Then
            character.SetTag(TagType.CanRecover, False)
            Dim illness = character.GetStatistic(StatisticType.Illness)
            result.Add(New DialogLine(MoodType.Danger, $"-{illness} HLT due to illness."))
            character.Platform.PlaySfx(Sfx.PlayerHit)
            character.ChangeStatistic(StatisticType.Health, -illness)
            character.ChangeStatistic(StatisticType.Illness, -1)
        End If
        Return result
    End Function

    Private Shared Function ProcessDehydration(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim result As New List(Of IDialogLine)
        If character.IsStatisticAtMinimum(StatisticType.Hydration) Then
            result.Add(New DialogLine(MoodType.Danger, "Yer dehydrated! Drink immediately!"))
        Else
            If character.ChangeStatistic(StatisticType.Hydration, -1) < HYDRATION_WARNING Then
                result.Add(New DialogLine(MoodType.Warning, "Yer thirsty! Drink soon!"))
            End If
        End If
        Return result
    End Function

    Private Shared Function ProcessHunger(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim result As New List(Of IDialogLine)
        If character.IsStatisticAtMinimum(StatisticType.Satiety) Then
            result.Add(New DialogLine(MoodType.Danger, "Yer starving! Eat immediately!"))
        Else
            If character.ChangeStatistic(StatisticType.Satiety, -1) < SATIETY_WARNING Then
                result.Add(New DialogLine(MoodType.Warning, "Yer famished! Eat soon!"))
            End If
        End If
        Return result
    End Function

    Private Shared Function ProcessStarvation(character As ICharacter) As IEnumerable(Of IDialogLine)
        If character.IsStatisticAtMinimum(StatisticType.Satiety) OrElse
            character.IsStatisticAtMinimum(StatisticType.Hydration) Then
            character.Platform.PlaySfx(Sfx.PlayerHit)
            character.SetTag(TagType.CanRecover, False)
            character.ChangeStatistic(StatisticType.Health, -1)
        End If
        If character.IsDead Then
            Return {New DialogLine(MoodType.Danger, "Yer dead.")}
        End If
        Return Array.Empty(Of IDialogLine)
    End Function

    Friend Overrides Sub OnLeave(character As ICharacter, location As ILocation)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return True
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return Not location.HasCharacter AndAlso location.LocationType = NameOf(GrassLocationTypeDescriptor)
    End Function

    Friend Overrides Sub HandleAddItem(character As ICharacter, item As IItem)
        item.Descriptor.HandleAddItem(item, character)
    End Sub

    Friend Overrides Sub HandleRemoveItem(character As ICharacter, item As IItem)
        item.Descriptor.HandleRemoveItem(item, character)
    End Sub

    Friend Overrides Function OnInteract(target As ICharacter, initiator As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function GetName(character As Character) As String
        Return CharacterTypeName
    End Function
End Class
