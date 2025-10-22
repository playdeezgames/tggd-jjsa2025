Imports TGGD.Business

Friend Class EnterViewModeVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(EnterViewModeVerbTypeDescriptor),
            Business.VerbCategoryType.Action,
            "View...")
    End Sub

    Friend Overrides Function Perform(character As ICharacter) As IDialog
        character.World.SetTag(TagType.ViewMode, True)
        character.World.SetStatisticRange(StatisticType.ViewColumn, character.Column, 0, character.Map.Columns - 1)
        character.World.SetStatisticRange(StatisticType.ViewRow, character.Row, 0, character.Map.Rows - 1)
        Return Nothing
    End Function

    Friend Overrides Function CanPerform(character As ICharacter) As Boolean
        Return MyBase.CanPerform(character) AndAlso Not character.World.GetTag(TagType.ViewMode)
    End Function
End Class
