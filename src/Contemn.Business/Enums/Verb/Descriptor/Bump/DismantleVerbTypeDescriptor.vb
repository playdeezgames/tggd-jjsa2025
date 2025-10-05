Imports TGGD.Business

Friend Class DismantleVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            Business.VerbType.Dismantle,
            Business.VerbCategoryType.Bump,
            "Dismantle")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New ConfirmDialog(
            "Confirm Dismantle?",
            {
                New DialogLine(MoodType.Warning, $"Are you sure you want"),
                New DialogLine(MoodType.Warning, $"to dismantle {character.GetBumpLocation().Name}?")
            },
            ConfirmDismantle(character),
            BumpDialog.LaunchMenu(character))
    End Function

    Private Function ConfirmDismantle(character As ICharacter) As Func(Of IDialog)
        Return Function() New OkDialog(
            character.World.ProcessTurn().Concat(DoDismantle(character)),
            Function() Nothing)
    End Function

    Private Function DoDismantle(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim bumpLocation = character.GetBumpLocation()
        Dim result As New List(Of IDialogLine) From {
            New DialogLine(MoodType.Info, $"You dismantle {bumpLocation.Name}")
        }
        Dim dismantleGenerator = bumpLocation.GetDismantleTable()
        For Each itemType In dismantleGenerator.Keys
            Dim itemCount = dismantleGenerator.GetWeight(itemType)
            For Each dummy In Enumerable.Range(0, itemCount)
                character.World.CreateItem(itemType, character)
            Next
            result.Add(New DialogLine(MoodType.Info, $"+{itemCount} {ItemTypes.Descriptors(itemType).ItemTypeName}({character.GetCountOfItemType(itemType)})"))
        Next
        bumpLocation.LocationType = bumpLocation.GetMetadata(MetadataType.DismantledLocationType)
        Return result
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(TagType.CanDismantle) AndAlso
            bumpLocation.IsStatisticAtMinimum(StatisticType.Fuel)
    End Function
End Class
