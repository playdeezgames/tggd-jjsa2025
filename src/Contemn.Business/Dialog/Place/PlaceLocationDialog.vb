Imports TGGD.Business

Friend Class PlaceLocationDialog
    Inherits LegacyBaseDialog

    Private ReadOnly character As ICharacter
    Private ReadOnly item As IItem

    Public Sub New(character As ICharacter, item As IItem)
        MyBase.New(
            GenerateCaption(),
            GenerateChoices(character),
            GenerateLines())
        Me.character = character
        Me.item = item
    End Sub

    Private Shared Function GenerateLines() As IEnumerable(Of IDialogLine)
        Return Array.Empty(Of IDialogLine)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Dim location = character.Location
        For Each descriptor In DirectionTypes.Descriptors.Values
            Dim nextColumn = descriptor.GetNextColumn(location.Column)
            Dim nextRow = descriptor.GetNextRow(location.Row)
            Dim nextLocation = character.Map.GetLocation(nextColumn, nextRow)
            If nextLocation IsNot Nothing AndAlso nextLocation.GetTag(TagType.IsPlaceable) Then
                result.Add(New DialogChoice(nextLocation.LocationId.ToString, $"{descriptor.Name}({nextLocation.Name})"))
            End If
        Next
        Return result
    End Function

    Private Shared Function GenerateCaption() As String
        Return "Placement Direction..."
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return PlaceItem(character, item, character.World.GetLocation(CInt(choice)))
        End Select
    End Function

    Private Shared Function PlaceItem(character As ICharacter, item As IItem, location As ILocation) As IDialog
        character.RemoveItem(item)
        item.Place(location)
        Return Nothing
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return PlaceItemDialog.LaunchMenu(character).Invoke
    End Function
End Class
