Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly FORAGE_AGAIN_CHOICE As String = NameOf(FORAGE_AGAIN_CHOICE)
    Private Const FORAGE_AGAIN_TEXT = "Forage Again..."
    Friend Shared ReadOnly FindNothingLines As IDialogLine() = {New DialogLine(MoodType.Info, "You find nothing.")}

    Public Sub New()
        MyBase.New(Business.VerbType.Forage, Business.VerbCategoryType.Action, "Forage...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim generator = character.Location.GetForageGenerator()
        Dim item = generator.GenerateItem(character)
        Dim lines = character.World.ProcessTurn()
        If item IsNot Nothing Then
            Return FoundItem(character, item, generator, lines)
        Else
            Return FoundNothing(character, lines)
        End If
    End Function

    Private Function FoundNothing(character As ICharacter, lines As IEnumerable(Of IDialogLine)) As IDialog
        Dim messageLines As New List(Of IDialogLine)(FindNothingLines)
        messageLines.AddRange(lines)
        Dim messageChoices As New List(Of (Choice As String, Text As String, NextDialog As Func(Of IDialog), Enabled As Boolean)) From
            {
                (OK_CHOICE, OK_TEXT, VerbListDialog.LaunchMenu(character), True),
                (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
            }
        character.PlaySfx(Sfx.Shucks)
        Return New MessageDialog(
            messageLines,
            messageChoices,
            VerbListDialog.LaunchMenu(character))
    End Function

    Private Function FoundItem(character As ICharacter, item As IItem, generator As IGenerator, lines As IEnumerable(Of IDialogLine)) As IDialog
        Dim itemCount = character.GetCountOfItemType(item.ItemType)
        Dim messageLines As New List(Of IDialogLine) From
            {
                New DialogLine(MoodType.Info, $"+1 {item.Name}({itemCount})")
            }
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        messageLines.AddRange(lines)
        Dim messageChoices As New List(Of (Choice As String, Text As String, NextDialog As Func(Of IDialog), Enabled As Boolean)) From
            {
                (OK_CHOICE, OK_TEXT, VerbListDialog.LaunchMenu(character), True),
                (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
            }
        If generator.IsDepleted Then
            messageLines.Add(New DialogLine(MoodType.Warning, $"{character.Location.LocationType.ToLocationTypeDescriptor.LocationType} is now depleted."))
            character.Location.LocationType = LocationType.Dirt
            generator.Recycle()
        End If
        Return New MessageDialog(
            messageLines,
            messageChoices,
            VerbListDialog.LaunchMenu(character))
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso character.Location.HasMetadata(MetadataType.ForageTable)
    End Function
End Class
