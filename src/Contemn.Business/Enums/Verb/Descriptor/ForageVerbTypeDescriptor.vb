Imports TGGD.Business

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

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
        Return New MessageDialog({"You find nothing."}, {(OK_CHOICE, OK_TEXT, Function() Nothing)}, Function() Nothing)
    End Function

    Private Function FoundItem(character As ICharacter, item As IItem) As IDialog
        Return New MessageDialog({$"You find {item.Name}."}, {(OK_CHOICE, OK_TEXT, Function() Nothing)}, Function() Nothing)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.HasMetadata(MetadataType.ForageTable)
    End Function
End Class
