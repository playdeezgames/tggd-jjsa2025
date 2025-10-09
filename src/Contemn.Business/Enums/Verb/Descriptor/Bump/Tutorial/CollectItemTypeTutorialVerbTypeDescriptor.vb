Imports TGGD.Business

Friend Class CollectItemTypeTutorialVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Private ReadOnly itemType As String
    Private ReadOnly tagType As String
    Private ReadOnly failureLines As IEnumerable(Of IDialogLine)
    Private ReadOnly successLines As IEnumerable(Of IDialogLine)
    Private ReadOnly checkPrerequisites As Func(Of ICharacter, Boolean)

    Public Sub New(
                  verbType As String,
                  verbTypeName As String,
                  itemType As String,
                  tagType As String,
                  checkPrerequisites As Func(Of ICharacter, Boolean),
                  failureLines As IEnumerable(Of IDialogLine),
                  successLines As IEnumerable(Of IDialogLine))
        MyBase.New(
            verbType,
            Business.VerbCategoryType.Bump,
            verbTypeName)
        Me.itemType = itemType
        Me.failureLines = failureLines
        Me.successLines = successLines
        Me.tagType = tagType
        Me.checkPrerequisites = checkPrerequisites
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        If character.HasItemsOfType(itemType) Then
            Return Success(character)
        End If
        Return Failure(character)
    End Function

    Private Function Failure(character As ICharacter) As IDialog
        Return New OkDialog(
            "Fail",
            failureLines,
            BumpDialog.LaunchMenu(character))
    End Function

    Private Function Success(character As ICharacter) As IDialog
        character.SetTag(tagType, True)

        Return New OkDialog(
            "Success",
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
            Not character.GetTag(tagType) AndAlso
            checkPrerequisites(character)
    End Function
End Class
