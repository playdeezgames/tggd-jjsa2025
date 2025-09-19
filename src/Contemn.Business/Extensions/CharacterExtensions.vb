Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module CharacterExtensions
    <Extension>
    Friend Function HandleBump(character As ICharacter, location As ILocation) As IDialog
        character.SetBumpLocation(location)
        Dim result = character.CharacterType.ToCharacterTypeDescriptor.OnBump(character, location)
        character.ClearBumpLocation()
        Return result
    End Function
    <Extension>
    Friend Sub SetBumpLocation(character As ICharacter, location As ILocation)
        character.SetStatistic(StatisticType.BumpLocationId, location.LocationId)
    End Sub
    <Extension>
    Friend Function GetBumpLocation(character As ICharacter) As ILocation
        If Not character.HasStatistic(StatisticType.BumpLocationId) Then
            Return Nothing
        End If
        Return character.World.GetLocation(character.GetStatistic(StatisticType.BumpLocationId))
    End Function
    <Extension>
    Friend Sub ClearBumpLocation(character As ICharacter)
        character.SetStatistic(StatisticType.BumpLocationId, Nothing)
    End Sub
    <Extension>
    Friend Sub SetStatisticRange(
                                character As ICharacter,
                                statisticType As String,
                                statisticValue As Integer,
                                statisticMinimum As Integer,
                                statisticMaximum As Integer)
        character.SetStatisticMinimum(statisticType, statisticMinimum)
        character.SetStatisticMaximum(statisticType, statisticMaximum)
        character.SetStatistic(statisticType, statisticValue)
    End Sub
    <Extension>
    Friend Function IsStatisticAtMinimum(character As ICharacter, statisticType As String) As Boolean
        Return character.GetStatistic(statisticType) = character.GetStatisticMinimum(statisticType)
    End Function
    <Extension>
    Friend Sub HandleLeave(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnLeave(character, location)
    End Sub
    <Extension>
    Friend Sub HandleEnter(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnEnter(character, location)
    End Sub
    <Extension>
    Friend Function IsDead(character As ICharacter) As Boolean
        Return character.IsStatisticAtMinimum(StatisticType.Health)
    End Function
End Module
