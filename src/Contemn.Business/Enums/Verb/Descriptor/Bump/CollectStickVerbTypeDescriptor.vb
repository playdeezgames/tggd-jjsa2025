Imports System.Reflection.Metadata.Ecma335
Imports TGGD.Business

Friend Class CollectStickVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly COLLECT_ANOTHER_CHOICE As String = NameOf(COLLECT_ANOTHER_CHOICE)
    Private Const COLLECT_ANOTHER_TEXT = "Collect Another"

    Public Sub New()
        MyBase.New(Business.VerbType.CollectStick, Business.VerbCategoryType.Bump, "Collect Stick")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New MessageDialog(
            character.
                World.
                ProcessTurn().
                Concat(HandlePerform(character)),
            {
                (OK_CHOICE, OK_TEXT, Function() Nothing, True),
                (COLLECT_ANOTHER_CHOICE, COLLECT_ANOTHER_TEXT, Function() Perform(character), CanPerform(character))
            },
            Function() New BumpDialog(character))
    End Function

    Private Function HandlePerform(character As ICharacter) As IEnumerable(Of IDialogLine)
        Dim bumpLocation = character.GetBumpLocation()
        character.PlaySfx(Sfx.WooHoo)
        Dim result As New List(Of IDialogLine)
        Dim stick = character.World.CreateItem(NameOf(StickItemTypeDescriptor), character)
        bumpLocation.ChangeStatistic(StatisticType.Resource, -1)
        result.Add(New DialogLine(MoodType.Info, $"+1 {stick.Name}({character.GetCountOfItemType(stick.ItemType)})"))
        result.AddRange(TreeLocationTypeDescriptor.DepleteTree(bumpLocation))
        Return result
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.LocationType = NameOf(TreeLocationTypeDescriptor) AndAlso
            Not bumpLocation.IsStatisticAtMinimum(StatisticType.Resource)
    End Function
End Class
