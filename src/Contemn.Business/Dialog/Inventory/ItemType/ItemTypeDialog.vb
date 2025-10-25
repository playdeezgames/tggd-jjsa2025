Imports TGGD.Business

Friend Class ItemTypeDialog
    Inherits EntityDialog(Of ICharacter)

    Private ReadOnly itemType As String
    Private Shared ReadOnly CRAFT_CHOICE As String = NameOf(CRAFT_CHOICE)
    Private Const CRAFT_TEXT = "Craft..."
    Shared ReadOnly DISMANTLE_CHOICE As String = NameOf(DISMANTLE_CHOICE)
    Const DISMANTLE_TEXT = "Dismantle..."

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            character,
            Function(x) ItemTypes.Descriptors(itemType).ItemTypeName,
            Function(x) GenerateChoices(x, itemType),
            Function(x) GenerateLines(x, itemType),
            Function() VerbTypes.Descriptors(NameOf(InventoryVerbTypeDescriptor)).Perform(character))
        Me.itemType = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of IDialogLine)
        Return {New DialogLine(MoodType.Info, $"You have {character.GetCountOfItemType(itemType)}.")}
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of IDialogChoice)
        Dim itemCount = character.GetCountOfItemType(itemType)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        result.AddRange(character.GetItemOfType(itemType).GetAvailableChoices(character))
        If RecipeTypes.Descriptors.Values.Any(Function(x) x.HasInput(itemType) AndAlso x.CanCraft(character)) Then
            result.Add(New DialogChoice(CRAFT_CHOICE, CRAFT_TEXT))
        End If
        If ItemTypes.Descriptors(itemType).DepletionTable.Any Then
            result.Add(New DialogChoice(DISMANTLE_CHOICE, DISMANTLE_TEXT))
        End If
        result.Add(New DialogChoice(DROP_ONE_CHOICE, DROP_ONE_TEXT))
        If itemCount \ 2 > 0 Then
            result.Add(New DialogChoice(DROP_HALF_CHOICE, $"{DROP_HALF_TEXT}({itemCount \ 2})"))
        End If
        If itemCount > 1 Then
            result.Add(New DialogChoice(DROP_ALL_CHOICE, DROP_ALL_TEXT))
        End If
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case CRAFT_CHOICE
                Return New ItemTypeCraftDialog(entity, itemType)
            Case DROP_ONE_CHOICE
                Return DropOne()
            Case DROP_HALF_CHOICE
                Return DropHalf()
            Case DROP_ALL_CHOICE
                Return DropAll()
            Case DISMANTLE_CHOICE
                Return Dismantle()
            Case Else
                Return entity.GetItemOfType(itemType).MakeChoice(entity, choice)
        End Select
    End Function

    Private Function Dismantle() As IDialog
        Return ItemTypeDismantleDialog.LaunchMenu(entity, itemType).Invoke
    End Function

    Private Function DropAll() As IDialog
        Return Drop(entity.GetCountOfItemType(itemType))
    End Function

    Private Function Drop(itemCount As Integer) As IDialog
        Dim descriptor = ItemTypes.Descriptors(itemType)
        For Each item In entity.ItemsOfType(itemType).Take(itemCount)
            entity.RemoveItem(item)
            entity.Location.AddItem(item)
        Next
        Return New OkDialog(
            "You dropped em!",
            {New DialogLine(MoodType.Info, $"You drop {itemCount} {descriptor.ItemTypeName}.")},
            ItemTypeDialog.LaunchMenu(entity, itemType))
    End Function

    Private Function DropHalf() As IDialog
        Return Drop(entity.GetCountOfItemType(itemType) \ 2)
    End Function

    Private Function DropOne() As IDialog
        Return Drop(1)
    End Function
    Friend Shared Function LaunchMenu(character As ICharacter, itemType As String) As Func(Of IDialog)
        Return Function() If(
            character.HasItemsOfType(itemType),
            CType(New ItemTypeDialog(character, itemType), IDialog),
            InventoryDialog.LaunchMenu(character).Invoke)
    End Function
End Class
