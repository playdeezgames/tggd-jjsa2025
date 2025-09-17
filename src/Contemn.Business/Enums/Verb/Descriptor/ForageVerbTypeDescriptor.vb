Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly FORAGE_AGAIN_CHOICE As String = NameOf(FORAGE_AGAIN_CHOICE)
    Private Const FORAGE_AGAIN_TEXT = "Forage Again..."
    Friend Shared ReadOnly FindNothingLines As String() = {"You find nothing."}

    Public Sub New()
        MyBase.New(Business.VerbType.Forage, "Forage...")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim generator = character.Location.GetForageGenerator()
        Dim item = generator.GenerateItem(character)
        If item IsNot Nothing Then
            Return FoundItem(character, item)
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

    Private Function FoundItem(character As ICharacter, item As IItem) As IDialog
        Dim itemCount = character.GetCountOfItemType(item.ItemType)
        Return New MessageDialog(
            {$"You find {item.Name}.",
            $"You now have {itemCount}."},
            {(OK_CHOICE, OK_TEXT, BackToGame(character)),
            (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character))},
            BackToGame(character))
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.HasMetadata(MetadataType.ForageTable)
    End Function
End Class
