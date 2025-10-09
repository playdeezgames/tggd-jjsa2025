Imports TGGD.Business

Friend Class GroundItemTypeDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter
    Private ReadOnly itemType As String
    Shared ReadOnly TAKE_ONE_CHOICE As String = NameOf(TAKE_ONE_CHOICE)
    Const TAKE_ONE_TEXT = "Take One"
    Shared ReadOnly TAKE_HALF_CHOICE As String = NameOf(TAKE_HALF_CHOICE)
    Const TAKE_HALF_TEXT = "Take Half"

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            ItemTypes.Descriptors(itemType).ItemTypeName,
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of IDialogLine)
        Return {New DialogLine(MoodType.Info, $"You see {character.Location.GetCountOfItemType(itemType)}.")}
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of IDialogChoice)
        Dim itemCount = character.Location.GetCountOfItemType(itemType)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT),
                New DialogChoice(TAKE_ONE_CHOICE, TAKE_ONE_TEXT)
            }
        If itemCount \ 2 > 0 Then
            result.Add(New DialogChoice(TAKE_HALF_CHOICE, $"{TAKE_HALF_TEXT}({itemCount \ 2})"))
        End If
        If itemCount > 1 Then
            result.Add(New DialogChoice(TAKE_ALL_CHOICE, TAKE_ALL_TEXT))
        End If
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case TAKE_ONE_CHOICE
                Return TakeOne()
            Case TAKE_HALF_CHOICE
                Return TakeHalf()
            Case TAKE_ALL_CHOICE
                Return TakeAll()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function TakeAll() As IDialog
        Return Take(character.Location.GetCountOfItemType(itemType))
    End Function

    Private Function Take(itemCount As Integer) As IDialog
        Dim descriptor = ItemTypes.Descriptors(itemType)
        For Each item In character.Location.ItemsOfType(itemType).Take(itemCount)
            character.Location.RemoveItem(item)
            character.AddItem(item)
        Next
        Return New OkDialog("You took em!", {New DialogLine(MoodType.Info, $"You take {itemCount} {descriptor.ItemTypeName}.")}, GroundItemTypeDialog.LaunchMenu(character, itemType))
    End Function

    Private Function TakeHalf() As IDialog
        Return Take(character.Location.GetCountOfItemType(itemType) \ 2)
    End Function

    Private Function TakeOne() As IDialog
        Return Take(1)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return GroundDialog.LaunchMenu(character).Invoke
    End Function


    Friend Shared Function LaunchMenu(character As ICharacter, itemType As String) As Func(Of IDialog)
        Return Function() If(
            character.Location.HasItemsOfType(itemType),
            CType(New GroundItemTypeDialog(character, itemType), IDialog),
            GroundDialog.LaunchMenu(character).Invoke)
    End Function
End Class
