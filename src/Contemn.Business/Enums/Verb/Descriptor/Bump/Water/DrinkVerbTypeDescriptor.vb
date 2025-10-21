Imports TGGD.Business

Friend Class DrinkVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(DrinkVerbTypeDescriptor),
            Business.VerbCategoryType.Bump,
            "Drink")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        Dim hydrationDelta = character.GetStatisticMaximum(StatisticType.Hydration) - character.GetStatistic(StatisticType.Hydration)
        Dim messageLines As New List(Of IDialogLine) From
            {
                New DialogLine(MoodType.Info, $"+{hydrationDelta} hydration")
            }
        If RNG.GenerateBoolean(1, 1, Nothing) Then
            Dim illness = RNG.RollXDY(1, 4)
            character.ChangeStatistic(StatisticType.Illness, illness)
            messageLines.Add(New DialogLine(MoodType.Info, $"+{illness} illness"))
        End If
        character.ChangeStatistic(StatisticType.Hydration, hydrationDelta)
        Return New OkDialog("You drank it!", messageLines, Function() Nothing)
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        If Not MyBase.CanPerform(character) Then
            Return False
        End If
        Dim bumpLocation = character.GetBumpLocation()
        If bumpLocation Is Nothing Then
            Return False
        End If
        Return bumpLocation.LocationType = NameOf(WaterLocationTypeDescriptor)
    End Function
End Class
