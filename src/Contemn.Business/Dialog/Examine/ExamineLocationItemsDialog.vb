Imports TGGD.Business

Friend Class ExamineLocationItemsDialog
    Inherits BaseDialog

    Private ReadOnly location As ILocation

    Public Sub New(location As ILocation)
        MyBase.New(GenerateCaption(location), GenerateChoices(location), GenerateLines(location))
        Me.location = location
    End Sub

    Private Shared Function GenerateLines(location As ILocation) As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(location As ILocation) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        result.AddRange(location.Items.Select(Function(x) New DialogChoice(x.ItemId.ToString, x.Name)))
        Return result
    End Function

    Private Shared Function GenerateCaption(location As ILocation) As String
        Return $"Items: {location.Items.Count()}"
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return ChooseItem(CInt(choice))
        End Select
    End Function

    Private Function ChooseItem(itemId As Integer) As IDialog
        Return New ExamineLocationItemDialog(location, location.World.GetItem(itemId))
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return New ExamineLocationDialog(location)
    End Function
End Class
