Imports TGGD.Business

Friend Class InteractWaterDialog
    Inherits BaseDialog

    Shared ReadOnly DRINK_CHOICE As String = NameOf(DRINK_CHOICE)
    Const DRINK_TEXT = "Drink"

    Private ReadOnly character As ICharacter
    Private ReadOnly location As ILocation

    Public Sub New(character As ICharacter, location As ILocation)
        MyBase.New("Water", GenerateChoices(), GenerateLines())
        Me.character = character
        Me.location = location
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of String)
        Return Array.Empty(Of String)
    End Function

    Private Shared Function GenerateChoices() As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT),
                (DRINK_CHOICE, DRINK_TEXT)
            }
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return Nothing
            Case DRINK_CHOICE
                Return Drink()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Drink() As IDialog
        Dim hydrationDelta = character.GetStatisticMaximum(StatisticType.Hydration) - character.GetStatistic(StatisticType.Hydration)
        Dim messageLines As New List(Of String) From
            {
                $"+{hydrationDelta} hydration"
            }
        If RNG.GenerateBoolean(1, 1) Then
            Dim illness = RNG.RollXDY(1, 4)
            character.ChangeStatistic(StatisticType.Illness, illness)
            messageLines.Add($"+{illness} illness")
        End If
        character.ChangeStatistic(StatisticType.Hydration, hydrationDelta)
        Return New OkDialog(messageLines, Function() Nothing)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
