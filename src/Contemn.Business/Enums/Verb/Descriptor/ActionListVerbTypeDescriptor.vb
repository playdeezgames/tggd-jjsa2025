Imports TGGD.Business

Friend Class ActionListVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.ActionList, Business.VerbCategoryType.Action, Nothing)
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New VerbListDialog(character, Business.VerbCategoryType.Action)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function
End Class
