Imports TGGD.Business

Friend Class RefuelItemDialog
    Inherits BaseDialog
    ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(
            GenerateCaption(character),
            GenerateChoices(character),
            GenerateLines(character))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Return {New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)}
    End Function

    Private Shared Function GenerateCaption(character As ICharacter) As String
        Return "Refuel With..."
    End Function

    Public Overrides Function Choose(choice As String) As TGGD.Business.IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Overrides Function CancelDialog() As TGGD.Business.IDialog
        Return New BumpDialog(
                Character,
                {
                    New DialogLine(MoodType.Info, Character.GetBumpLocation().FormatStatistic(StatisticType.Fuel))
                })
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
            VerbType.Refuel.ToVerbTypeDescriptor.CanPerform(character),
            CType(New RefuelItemDialog(character), IDialog),
            CType(New BumpDialog(
                character,
                {
                    New DialogLine(MoodType.Info, character.GetBumpLocation().FormatStatistic(StatisticType.Fuel))
                }), IDialog))
    End Function
End Class
