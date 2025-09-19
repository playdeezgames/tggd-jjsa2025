Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly FORAGE_AGAIN_CHOICE As String = NameOf(FORAGE_AGAIN_CHOICE)
    Private Const FORAGE_AGAIN_TEXT = "Forage Again..."
    Friend Shared ReadOnly FindNothingLines As (Mood As String, Text As String)() = {(MoodType.Info, "You find nothing.")}

    Public Sub New()
        MyBase.New(Business.VerbType.Forage, Business.VerbCategoryType.Action, "Forage...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim generator = character.Location.GetForageGenerator()
        Dim item = generator.GenerateItem(character)
        Dim lines = character.ProcessTurn()
        If item IsNot Nothing Then
            Return FoundItem(character, item, generator, lines)
        Else
            Return FoundNothing(character, lines)
        End If
    End Function

    Private Function FoundNothing(character As ICharacter, lines As IEnumerable(Of (Mood As String, Text As String))) As IDialog
        Dim messageLines As New List(Of (Mood As String, Text As String))(FindNothingLines)
        messageLines.AddRange(lines)
        Dim messageChoices As New List(Of (Choice As String, Text As String, NextDialog As Func(Of IDialog), Enabled As Boolean)) From
            {
                (OK_CHOICE, OK_TEXT, BackToGame(), True),
                (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character), Not character.IsDead)
            }
        Return New MessageDialog(
            messageLines,
            messageChoices,
            BackToGame())
    End Function

    Private Shared Function ForageAgain(character As ICharacter) As Func(Of IDialog)
        Return Function() Business.VerbType.Forage.ToVerbTypeDescriptor.Perform(character)
    End Function

    Private Shared Function BackToGame() As Func(Of IDialog)
        Return Function() Nothing
    End Function

    Private Function FoundItem(character As ICharacter, item As IItem, generator As IGenerator, lines As IEnumerable(Of (Mood As String, Text As String))) As IDialog
        Dim itemCount = character.GetCountOfItemType(item.ItemType)
        Dim messageLines As New List(Of (Mood As String, Text As String)) From
            {
                (MoodType.Info, $"You find {item.Name}."),
                (MoodType.Info, $"You now have {itemCount}.")
            }
        messageLines.AddRange(lines)
        Dim messageChoices As New List(Of (Choice As String, Text As String, NextDialog As Func(Of IDialog), Enabled As Boolean)) From
            {
                (OK_CHOICE, OK_TEXT, BackToGame(), True),
                (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character), Not character.IsDead AndAlso Not generator.IsDepleted)
            }
        If generator.IsDepleted Then
            messageLines.Add((MoodType.Warning, $"{character.Location.LocationType.ToLocationTypeDescriptor.LocationType} is now depleted."))
            character.Location.LocationType = LocationType.Dirt
            generator.Recycle()
        End If
        Return New MessageDialog(
            messageLines,
            messageChoices,
            BackToGame())
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.HasMetadata(MetadataType.ForageTable)
    End Function
End Class
