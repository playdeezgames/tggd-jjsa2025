Imports TGGD.Business

Friend Class ActionListVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.ActionList, Nothing)
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New ActionListDialog(character)
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function
End Class
