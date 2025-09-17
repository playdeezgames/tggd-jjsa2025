Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly FORAGE_AGAIN_CHOICE As String = NameOf(FORAGE_AGAIN_CHOICE)
    Private Const FORAGE_AGAIN_TEXT = "Forage Again..."

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
            {"You find nothing."},
            {(OK_CHOICE, OK_TEXT, BackToGame(character)),
            (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character))},
            BackToGame(character))
    End Function

    Private Function ForageAgain(character As ICharacter) As Func(Of IDialog)
        Return Function() Business.VerbType.Forage.ToVerbTypeDescriptor.Perform(character)
    End Function

    Private Function BackToGame(character As ICharacter) As Func(Of IDialog)
        Return Function() Nothing
    End Function

    Private Function FoundItem(character As ICharacter, item As IItem) As IDialog
        Return New MessageDialog(
            {$"You find {item.Name}."},
            {(OK_CHOICE, OK_TEXT, BackToGame(character)),
            (FORAGE_AGAIN_CHOICE, FORAGE_AGAIN_TEXT, ForageAgain(character))},
            BackToGame(character))
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.HasMetadata(MetadataType.ForageTable)
    End Function
End Class
