Imports TGGD.Business

Friend Class ActionListVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.ActionList, Business.VerbCategoryType.Action, Nothing)
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return VerbListDialog.LaunchMenu(character).Invoke()
    End Function
End Class
