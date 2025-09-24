Imports TGGD.Business

Friend Class ChopWoodVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Const RESOURCE_PER_LOG = 5
    Const CHOP_AGAIN_TEXT = "Chop Again"

    Public Sub New()
        MyBase.New(
            Business.VerbType.ChopWood,
            Business.VerbCategoryType.Bump,
            "Chop Wood")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Dim bumpLocation = character.GetBumpLocation()
        Dim tool = character.World.CreateItem(NameOf(LogItemTypeDescriptor), character)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -RESOURCE_PER_LOG)
        character.ChangeStatistic(StatisticType.Score, 1)
        character.PlaySfx(Sfx.WooHoo)
        Return New MessageDialog(
                    character.ProcessTurn().
                        Append(New DialogLine(MoodType.Info, $"+1 {tool.Name}({character.GetCountOfItemType(NameOf(LogItemTypeDescriptor))})")).
                        Concat(character.Items.First(Function(x) x.GetTag(TagType.CanChop)).Deplete(character)).
                        Concat(TreeLocationTypeDescriptor.DepleteTree(bumpLocation)),
                    {
                        (OK_CHOICE, OK_TEXT, Function() New BumpDialog(character), True),
                        (TRY_AGAIN_CHOICE, CHOP_AGAIN_TEXT, Function() Perform(character), CanPerform(character))
                    },
                    Function() Nothing)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation.GetTag(TagType.IsChoppable) AndAlso
            bumpLocation.GetStatistic(StatisticType.Resource) >= RESOURCE_PER_LOG AndAlso
            character.Items.Any(Function(x) x.GetTag(TagType.CanChop))
    End Function
End Class
