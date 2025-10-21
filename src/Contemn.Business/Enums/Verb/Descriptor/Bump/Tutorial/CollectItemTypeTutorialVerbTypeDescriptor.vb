Imports TGGD.Business

Friend Class CollectItemTypeTutorialVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Private ReadOnly requiredItemType As String
    Private ReadOnly achievementTagType As String
    Private ReadOnly failureLines As IEnumerable(Of IDialogLine)
    Private ReadOnly successLines As IEnumerable(Of IDialogLine)
    Private ReadOnly prerequisiteTagTypes As HashSet(Of String)

    Public Sub New(
                  verbTypeName As String,
                  requiredItemType As String,
                  prerequisiteItemTypes As IEnumerable(Of String),
                  failureLines As IEnumerable(Of String),
                  successLines As IEnumerable(Of String))
        MyBase.New(
            NameOf(CollectItemTypeTutorialVerbTypeDescriptor).
                Replace(
                    "ItemType",
                    requiredItemType.Replace("ItemTypeDescriptor", String.Empty)),
            Business.VerbCategoryType.Bump,
            $"Tutorial: {verbTypeName}")
        Me.requiredItemType = requiredItemType
        Me.failureLines = Enumerable.Range(0, failureLines.Count).
            Select(Function(x) New DialogLine(MoodType.Info, $"{x + 1}. {failureLines.ToArray(x)}"))
        Me.successLines = successLines.Select(Function(x) New DialogLine(MoodType.Info, x))
        Me.achievementTagType = TagType.CompletedCollectTutorial(requiredItemType)
        Me.prerequisiteTagTypes = New HashSet(Of String)(prerequisiteItemTypes.Select(AddressOf TagType.CompletedCollectTutorial))
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        If character.HasItemsOfType(requiredItemType) Then
            Return Success(character)
        End If
        Return Failure(character)
    End Function

    Private Function Failure(character As ICharacter) As IDialog
        Return New OkDialog(
            VerbTypeName,
            failureLines,
            BumpDialog.LaunchMenu(character))
    End Function

    Private Function Success(character As ICharacter) As IDialog
        character.SetTag(achievementTagType, True)

        Return New OkDialog(
            VerbTypeName,
            successLines.
            Concat(RestoreStats(character)),
            BumpDialog.LaunchMenu(character))
    End Function

    Private Shared Function RestoreStats(character As ICharacter) As IEnumerable(Of DialogLine)
        Return {
                New DialogLine(MoodType.Info, "Reward:"),
                Restore(character, StatisticType.Hydration),
                Restore(character, StatisticType.Satiety)
            }
    End Function

    Private Shared Function Restore(character As ICharacter, statisticType As String) As DialogLine
        Dim delta = character.GetStatisticMaximum(statisticType) - character.GetStatistic(statisticType)
        character.ChangeStatistic(statisticType, delta)
        Return New DialogLine(MoodType.Info, $"+{delta} {character.FormatStatistic(statisticType)}")
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Dim bumpLocation = character.GetBumpLocation()
        Return MyBase.CanPerform(character) AndAlso
            bumpLocation IsNot Nothing AndAlso
            bumpLocation.GetTag(Business.TagType.IsTutorialHouse) AndAlso
            Not character.GetTag(achievementTagType) AndAlso
            CheckPrerequisites(character)
    End Function

    Private Function CheckPrerequisites(character As ICharacter) As Boolean
        Return prerequisiteTagTypes.All(Function(x) character.GetTag(x))
    End Function
End Class
