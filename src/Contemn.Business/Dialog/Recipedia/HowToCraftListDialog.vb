Imports TGGD.Business

Friend Class HowToCraftListDialog
    Inherits CharacterDialog

    Public Sub New(character As ICharacter)
        MyBase.New(
            character,
            Function(x) GenerateCaption(),
            Function(x) GenerateChoices(),
            Function(x) GenerateLines(),
            Function() New RecipediaDialog(character))
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices() As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Dim outputs = New HashSet(Of String)(RecipeTypes.Descriptors.SelectMany(Function(x) x.OutputItemTypes))
        result.AddRange(outputs.Select(Function(x) New DialogChoice(x, ItemTypes.Descriptors(x).ItemTypeName)).OrderBy(Function(x) x.Text))
        Return result
    End Function

    Private Shared Function GenerateCaption() As String
        Return "How to craft...?"
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return New HowToCraftItemTypeDialog(character, choice)
        End Select
    End Function
End Class
