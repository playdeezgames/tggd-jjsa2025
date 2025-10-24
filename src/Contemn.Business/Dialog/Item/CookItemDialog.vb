Imports TGGD.Business

Friend Class CookItemDialog
    Inherits EntityDialog(Of ICharacter)

    Public Sub New(character As ICharacter)
        MyBase.New(
            character,
            Function(x) "Cook...",
            AddressOf GenerateChoices,
            AddressOf GenerateLines,
            BumpDialog.LaunchMenu(character))
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
                Return DoCook(entity.World.GetItem(CInt(choice)))
        End Select
    End Function

    Private Function DoCook(item As IItem) As IDialog
        Dim itemType = item.ItemType
        Dim cookedItemType = item.GetMetadata(MetadataType.CookedItemType)
        Dim cookedItem = entity.World.CreateItem(cookedItemType, entity)
        entity.RemoveItem(item)
        Dim messageLines = entity.World.ProcessTurn().
            Append(New DialogLine(MoodType.Info, $"-1 {item.Descriptor.ItemTypeName}({entity.GetCountOfItemType(itemType)})")).
            Append(New DialogLine(MoodType.Info, $"+1 {cookedItem.Descriptor.ItemTypeName}({entity.GetCountOfItemType(cookedItemType)})"))
        item.Recycle()
        Return New OkDialog(
            "You cooked it!",
            messageLines,
            LaunchMenu(entity))
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
