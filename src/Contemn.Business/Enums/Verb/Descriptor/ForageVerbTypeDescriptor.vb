Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly FORAGE_AGAIN_CHOICE As String = NameOf(FORAGE_AGAIN_CHOICE)
    Private Const FORAGE_AGAIN_TEXT = "Forage Again..."
    Friend Shared ReadOnly FindNothingLines As String() = {"You find nothing."}

    Public Sub New()
        MyBase.New(Business.VerbType.Forage, Business.VerbCategoryType.Action, "Forage...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim generator = character.Location.GetForageGenerator()
        Dim item = generator.GenerateItem(character)
        If item IsNot Nothing Then
            Return FoundItem(character, item, generator)
        Else
            Return FoundNothing(character)
        End If
    End Function

    Private Function FoundNothing(character As ICharacter) As IDialog
        Return New MessageDialog(
            FindNothingLines,
            {(OK_CHOICE, OK_TEXT, BackToGame(character)),
            (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character))},
            BackToGame(character))
    End Function

    Private Shared Function ForageAgain(character As ICharacter) As Func(Of IDialog)
        Return Function() Business.VerbType.Forage.ToVerbTypeDescriptor.Perform(character)
    End Function

    Private Shared Function BackToGame(character As ICharacter) As Func(Of IDialog)
        ArgumentNullException.ThrowIfNull(character)
        Return Function() Nothing
    End Function

    Private Function FoundItem(character As ICharacter, item As IItem, generator As IGenerator) As IDialog
        Dim itemCount = character.GetCountOfItemType(item.ItemType)
        Dim messageLines As New List(Of String) From
            {
                $"You find {item.Name}.",
                $"You now have {itemCount}."
            }
        Dim messageChoices As New List(Of (Choice As String, Text As String, NextDialog As Func(Of IDialog))) From
            {
                (OK_CHOICE, OK_TEXT, BackToGame(character))
            }
        If generator.IsDepleted Then
            messageLines.Add($"{character.Location.LocationType.ToLocationTypeDescriptor.LocationType} is now depleted.")
            character.Location.LocationType = LocationType.Dirt
            generator.Recycle()
        Else
            messageChoices.Add((FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character)))
        End If
        Return New MessageDialog(
            messageLines,
            messageChoices,
            BackToGame(character))
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.HasMetadata(MetadataType.ForageTable)
    End Function
End Class
