Imports TGGD.Business

Friend Class DismantleVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    ReadOnly dismantleTable As IReadOnlyDictionary(Of String, IReadOnlyDictionary(Of String, Integer)) =
        New Dictionary(Of String, IReadOnlyDictionary(Of String, Integer)) From
        {
            {
                LocationType.CampFire,
                New Dictionary(Of String, Integer) From {
                    {NameOf(RockItemTypeDescriptor), 4}
                }
            },
            {
                LocationType.Kiln,
                New Dictionary(Of String, Integer) From {
                    {NameOf(RockItemTypeDescriptor), 4}
                }
            }
        }

    Public Sub New()
        MyBase.New(
            Business.VerbType.Dismantle,
            Business.VerbCategoryType.Bump,
            "Dismantle")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim messageLines = character.World.ProcessTurn().Concat(DoDismantle(character))
        Return New OkDialog(
            messageLines,
            Function() Nothing)
    End Function

    Private Function DoDismantle(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim bumpLocation = character.GetBumpLocation()
        Dim result As New List(Of IDialogLine) From {
            New DialogLine(MoodType.Info, $"You dismantle {bumpLocation.Name}")
        }
        For Each entry In dismantleTable(bumpLocation.LocationType)
            Dim itemType = entry.Key
            For Each dummy In Enumerable.Range(0, entry.Value)
                character.World.CreateItem(itemType, character)
            Next
            result.Add(New DialogLine(MoodType.Info, $"+{entry.Value} {itemType.ToItemTypeDescriptor.ItemTypeName}({character.GetCountOfItemType(itemType)})"))
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
