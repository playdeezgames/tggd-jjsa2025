Imports TGGD.Business

Friend Class HowToCraftListDialog
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
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Dim outputs = New HashSet(Of String)(RecipeTypes.Descriptors.Values.SelectMany(Function(x) x.OutputItemTypes))
        result.AddRange(outputs.Select(Function(x) New DialogChoice(x, ItemTypes.Descriptors(x).ItemTypeName)).OrderBy(Function(x) x.Text))
        Return result
    End Function

    Private Shared Function GenerateCaption(character As ICharacter) As String
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

    Public Overrides Function CancelDialog() As IDialog
        Return New RecipediaDialog(character)
    End Function
End Class
