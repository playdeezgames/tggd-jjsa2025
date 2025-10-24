Imports TGGD.Business

Friend Class RefuelItemDialog
    Inherits LegacyBaseDialog
    ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(
            GenerateCaption(character),
            GenerateChoices(character),
            GenerateLines(character))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim location = character.GetBumpLocation()
        Return {
            New DialogLine(MoodType.Info, location.FormatStatistic(StatisticType.Fuel))
            }
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim statisticName = StatisticTypes.Descriptors(StatisticType.Fuel).StatisticTypeName
        Return {
            New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
        }.Concat(
            character.Items.
            Where(Function(x) x.GetTag(TagType.CanRefuel)).
            Select(Function(x) New DialogChoice(
                x.ItemId.ToString,
                $"{x.Name}(+{x.GetStatistic(Business.StatisticType.Fuel)} {statisticName})")))
    End Function

    Private Shared Function GenerateCaption(character As ICharacter) As String
        Return $"Refuel {character.GetBumpLocation().Name} With..."
    End Function

    Public Overrides Function Choose(choice As String) As TGGD.Business.IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return DoRefuel(character.World.GetItem(CInt(choice)))
        End Select
    End Function

    Private Function DoRefuel(item As IItem) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        bumpLocation.ChangeStatistic(StatisticType.Fuel, item.GetStatistic(StatisticType.Fuel))
        character.ChangeStatistic(StatisticType.Score, 1)
        character.RemoveAndRecycleItem(item)
        Return LaunchMenu(character).Invoke()
    End Function

    Public Overrides Function CancelDialog() As TGGD.Business.IDialog
        Return New BumpDialog(character)
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
            VerbTypes.Descriptors(NameOf(RefuelVerbTypeDescriptor)).CanPerform(character),
            CType(New RefuelItemDialog(character), IDialog),
            CType(New BumpDialog(character), IDialog))
    End Function
End Class
