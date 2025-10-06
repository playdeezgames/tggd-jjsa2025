Imports TGGD.Business

Public Class CookItemDialog
    Inherits BaseDialog

    Private Shared cookTable As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {
            {NameOf(RawFishFiletItemTypeDescriptor), NameOf(CookedFishFiletItemTypeDescriptor)},
            {NameOf(LogItemTypeDescriptor), NameOf(CharcoalItemTypeDescriptor)}
        }

    ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        MyBase.New(
            "Cook...",
            GenerateChoices(character),
            GenerateLines(character))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim bumpLocation = character.GetBumpLocation
        Return {
            New DialogLine(MoodType.Info, bumpLocation.Name),
            New DialogLine(MoodType.Info, bumpLocation.FormatStatistic(StatisticType.Fuel))
            }
    End Function

    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice) From
            {
                New DialogChoice(NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        For Each item In character.Items.Where(Function(x) x.GetTag(TagType.IsCookable))
            result.Add(New DialogChoice(item.ItemId.ToString, item.Name))
        Next
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return CancelDialog()
            Case Else
                Return DoCook(character.World.GetItem(CInt(choice)))
        End Select
    End Function

    Private Function DoCook(item As IItem) As IDialog
        Dim itemType = item.ItemType
        Dim cookedItemType = cookTable(itemType)
        character.RemoveAndRecycleItem(item)
        Dim cookedItem = character.World.CreateItem(cookedItemType, character)
        Return New OkDialog(
            character.World.ProcessTurn().
            Append(New DialogLine(MoodType.Info, $"-1 {item.Descriptor.ItemTypeName}({character.GetCountOfItemType(itemType)})")).
            Append(New DialogLine(MoodType.Info, $"+1 {cookedItem.Descriptor.ItemTypeName}({character.GetCountOfItemType(cookedItemType)})")),
            LaunchMenu(character))
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return New BumpDialog(character)
    End Function

    Friend Shared Function LaunchMenu(character As ICharacter) As Func(Of IDialog)
        Dim bumpLocation = character.GetBumpLocation
        Return Function() If(
            character.Items.Any(Function(x) x.GetTag(TagType.IsCookable)) AndAlso
                bumpLocation.GetTag(TagType.IsLit),
            CType(New CookItemDialog(character), IDialog),
            CType(New BumpDialog(character), IDialog))
    End Function
End Class
