Imports TGGD.Business

Friend Class PlaceItemDialog
    Inherits CharacterDialog

    Public Sub New(character As ICharacter)
        MyBase.New(
            character,
            Function(x) GenerateCaption(),
            AddressOf GenerateChoices,
            Function(x) GenerateLines(),
            CharacterActionsDialog.LaunchMenu(character))
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each item In character.Items.Where(Function(x) x.GetTag(TagType.CanPlace))
            result.Add(New DialogChoice(item.ItemId.ToString, item.Name))
        Next
        Return result
    End Function

    Private Shared Function GenerateCaption() As String
        Return "Place..."
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() If(
            VerbTypes.Descriptors(NameOf(PlaceVerbTypeDescriptor)).CanPerform(character),
            New PlaceItemDialog(character),
            CharacterActionsDialog.LaunchMenu(character).Invoke)
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CharacterActionsDialog.LaunchMenu(character).Invoke
            Case Else
                Return ChooseLocation(choice)
        End Select
    End Function

    Private Function ChooseLocation(choice As String) As IDialog
        Return New PlaceLocationDialog(character, character.World.GetItem(CInt(choice)))
    End Function
End Class
