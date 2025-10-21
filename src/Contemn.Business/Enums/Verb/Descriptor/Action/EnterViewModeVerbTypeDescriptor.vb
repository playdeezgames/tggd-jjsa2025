Imports TGGD.Business

Friend Class EnterViewModeVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(EnterViewModeVerbTypeDescriptor),
            Business.VerbCategoryType.Action,
            "View Mode")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        character.World.SetTag(TagType.ViewMode, True)
        character.World.SetStatistic(StatisticType.ViewColumn, character.Column)
        character.World.SetStatistic(StatisticType.ViewRow, character.Row)
        Return Nothing
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso Not character.World.GetTag(TagType.ViewMode)
    End Function
End Class
