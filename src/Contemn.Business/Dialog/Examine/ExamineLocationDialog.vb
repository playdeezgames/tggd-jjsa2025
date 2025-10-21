Imports TGGD.Business

Public Class ExamineLocationDialog
    Inherits BaseDialog
    Shared ReadOnly CHARACTER_CHOICE As String = NameOf(CHARACTER_CHOICE)
    Shared ReadOnly ITEMS_CHOICE As String = NameOf(ITEMS_CHOICE)
    Private ReadOnly location As ILocation
    Const ITEMS_TEXT = "Items..."

    Public Sub New(location As ILocation)
        MyBase.New(GenerateCaption(location), GenerateChoices(location), GenerateLines(location))
        Me.location = location
    End Sub

    Private Shared Function GenerateLines(location As ILocation) As IEnumerable(Of IDialogLine)
        Return location.Describe()
    End Function

    Private Shared Function GenerateChoices(location As ILocation) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        If location.Character IsNot Nothing Then
            result.Add(New DialogChoice(CHARACTER_CHOICE, $"Character: {location.Character.Name}"))
        End If
        If location.HasItems Then
            result.Add(New DialogChoice(ITEMS_CHOICE, ITEMS_TEXT))
        End If
        Return result
    End Function

    Private Shared Function GenerateCaption(location As ILocation) As String
        Return location.Name
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case CHARACTER_CHOICE
                Return New ExamineLocationCharacterDialog(location)
            Case ITEMS_CHOICE
                Return New ExamineLocationItemsDialog(location)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
