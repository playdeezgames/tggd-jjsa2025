Imports TGGD.Business

Friend MustInherit Class MendableItemTypeDescriptor
    Inherits ItemTypeDescriptor
    ReadOnly maximumDurability As Integer
    ReadOnly tags As IEnumerable(Of String)
    ReadOnly mendConsumptionItemType As String
    Private ReadOnly durabilityRepaired As Integer
    Private ReadOnly MendChoice As IDialogChoice = New DialogChoice("MEND_CHOICE", "Mend")

    Protected Sub New(
                     itemType As String,
                     itemTypeName As String,
                     itemCount As Integer,
                     isAggregate As Boolean,
                     maximumDurability As Integer,
                     tags As IEnumerable(Of String),
                     mendConsumptionItemType As String,
                     durabilityRepaired As Integer)
        MyBase.New(
            itemType,
            itemTypeName,
            itemCount,
            isAggregate)
        Me.maximumDurability = maximumDurability
        Me.tags = tags
        Me.mendConsumptionItemType = mendConsumptionItemType
        Me.durabilityRepaired = durabilityRepaired
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Sub HandleInitialize(item As IItem)
        item.SetStatisticRange(
            StatisticType.Durability,
            maximumDurability,
            0,
            maximumDurability)
        For Each tag In tags
            item.SetTag(tag, True)
        Next
    End Sub
    Friend Overrides Function GetName(item As IItem) As String
        Return $"{ItemTypeName}({item.GetStatistic(StatisticType.Durability)}/{item.GetStatisticMaximum(StatisticType.Durability)})"
    End Function


    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case MendChoice.Choice
                Return Mend(item, character)
            Case Else
                Return HandleOtherChoice(item, character, choice)
        End Select
    End Function

    Protected Overridable Function HandleOtherChoice(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException
    End Function

    Private Function Mend(item As IItem, character As ICharacter) As IDialog
        Return New OkDialog(
            GenerateMendLines(item, character),
            Function() New ItemOfTypeDialog(character, item))
    End Function

    Private Function GenerateMendLines(item As IItem, character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim result = character.World.ProcessTurn().ToList
        Dim originalDurability = item.GetStatistic(StatisticType.Durability)
        item.ChangeStatistic(StatisticType.Durability, durabilityRepaired)
        Dim mendDurability = item.GetStatistic(StatisticType.Durability) - originalDurability
        result.Add(New DialogLine(MoodType.Info, $"+{mendDurability} {StatisticType.Durability.ToStatisticTypeDescriptor.StatisticTypeName}({item.GetStatistic(StatisticType.Durability)}/{item.GetStatisticMaximum(StatisticType.Durability)})"))
        Dim mendIngredient = character.GetItemOfType(mendConsumptionItemType)
        character.RemoveItem(mendIngredient)
        result.Add(New DialogLine(MoodType.Info, $"-1 {mendIngredient.Name}({character.GetCountOfItemType(mendIngredient.ItemType)})"))
        mendIngredient.Recycle()
        Return result
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim choices As New List(Of IDialogChoice)
        If CanMend(item, character) Then
            choices.Add(MendChoice)
        End If
        Return choices
    End Function

    Private Function CanMend(item As IItem, character As ICharacter) As Boolean
        Return Not item.IsStatisticAtMaximum(StatisticType.Durability) AndAlso character.HasItemsOfType(mendConsumptionItemType)
    End Function

End Class
