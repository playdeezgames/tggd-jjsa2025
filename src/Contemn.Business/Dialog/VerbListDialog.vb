Imports TGGD.Business

Public Class VerbListDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter
    Sub New(character As ICharacter, verbCategoryType As String, caption As String)
        MyBase.New(
            caption,
            GenerateChoices(character, verbCategoryType),
            Array.Empty(Of (Mood As String, Text As String)))
        Me.character = character
    End Sub

    Public Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Return Function() CType(New VerbListDialog(character, VerbCategoryType.Action, "Actions..."), IDialog)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, verbCategoryType As String) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each verbType In character.AvailableVerbsOfCategory(verbCategoryType)
            Dim descriptor = verbType.ToVerbTypeDescriptor
            result.Add((verbType, descriptor.VerbTypeName))
        Next
        Return result
    End Function
    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return Nothing
            Case Else
                Return choice.ToVerbTypeDescriptor.Perform(character)
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Choose(NEVER_MIND_CHOICE)
    End Function
End Class
