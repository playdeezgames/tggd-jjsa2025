Imports TGGD.Business

Friend MustInherit Class ToolBumpVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    ReadOnly itemType As String
    ReadOnly toolTag As String
    ReadOnly locationTag As String
    ReadOnly validLocationTypes As IEnumerable(Of String)
    ReadOnly failMessage As String

    Protected Sub New(
                     verbType As String,
                     verbTypeName As String,
                     itemType As String,
                     toolTag As String,
                     locationTag As String,
                     validLocationTypes As IEnumerable(Of String),
                     failMessage As String)
        MyBase.New(
            verbType,
            Business.VerbCategoryType.Bump,
            verbTypeName)
        Me.itemType = itemType
        Me.toolTag = toolTag
        Me.locationTag = locationTag
        Me.validLocationTypes = validLocationTypes
        Me.failMessage = failMessage
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        Dim success = RNG.GenerateBoolean(bumpLocation.GetStatistic(StatisticType.Depletion), bumpLocation.GetStatistic(StatisticType.Resource))
        If success Then
            Return HandleSuccess(character, bumpLocation)
        End If
        Return HandleFailure(character)
    End Function

    Private Function HandleFailure(character As ICharacter) As IDialog
        character.PlaySfx(Sfx.Shucks)
        Return New MessageDialog(
            character.ProcessTurn().
                Append(New DialogLine(MoodType.Info, failMessage)),
            {
                (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                (TRY_AGAIN_CHOICE, TRY_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
            },
            Function() Nothing)
    End Function

    Private Function HandleSuccess(character As ICharacter, bumpLocation As ILocation) As IDialog
        Dim item = character.World.CreateItem(itemType, character)
        bumpLocation.ChangeStatistic(StatisticType.Depletion, 1)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -1)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        Return New MessageDialog(
                    character.ProcessTurn().
                        Append(New DialogLine(MoodType.Info, $"+1 {item.Name}({character.GetCountOfItemType(itemType)})")).
                        Concat(character.Items.First(Function(x) x.GetTag(toolTag)).Deplete(character)),
                    {
                        (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                        (TRY_AGAIN_CHOICE, TRY_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
                    },
                    Function() Nothing)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(locationTag) AndAlso
            validLocationTypes.Contains(bumpLocation.LocationType) AndAlso
            Not bumpLocation.IsStatisticAtMinimum(StatisticType.Resource) AndAlso
            character.Items.Any(Function(x) x.GetTag(toolTag))
    End Function
End Class
