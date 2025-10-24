Imports TGGD.Business

Friend Class ChooseFillVesselDialog
    Inherits CharacterDialog

    Public Sub New(character As ICharacter)
        MyBase.New(
            character,
            AddressOf GenerateCaption,
            AddressOf GenerateChoices,
            AddressOf GenerateLines,
            BumpDialog.LaunchMenu(character))
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        result.AddRange(
            character.Items.Where(AddressOf IsFillableItem).
            Select(Function(x) New DialogChoice(x.ItemId.ToString(), x.Name)))
        Return result
    End Function

    Private Shared Function GenerateCaption(character As ICharacter) As String
        Return "Fill..."
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return FillVessel(CInt(choice))
        End Select
    End Function

    Private Function FillVessel(itemId As Integer) As IDialog
        Dim item = character.World.GetItem(itemId)
        item.SetStatistic(StatisticType.Water, item.GetStatisticMaximum(StatisticType.Water))
        item.SetTag(TagType.Safe, False)
        Return New OkDialog(
            "You filled it!",
            character.World.ProcessTurn().
            Append(New DialogLine(MoodType.Info, $"Filled {item.Name}")),
            ChooseFillVesselDialog.LaunchMenu(character))
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Dim emptyDialog As IDialog = Nothing
        Return If(
            HasFillableItem(character),
            Function() New ChooseFillVesselDialog(character),
            BumpDialog.LaunchMenu(character))
    End Function

    Private Shared Function HasFillableItem(character As ICharacter) As Boolean
        Return character.Items.Any(AddressOf IsFillableItem)
    End Function

    Private Shared Function IsFillableItem(item As IItem) As Boolean
        Return item.GetTag(TagType.IsFillable) AndAlso Not item.IsStatisticAtMaximum(StatisticType.Water)
    End Function
End Class
