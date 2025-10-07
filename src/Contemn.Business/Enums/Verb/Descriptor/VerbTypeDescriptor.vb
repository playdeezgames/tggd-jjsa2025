Imports TGGD.Business

Public MustInherit Class VerbTypeDescriptor
    Friend ReadOnly Property VerbCategoryType As String
    Friend ReadOnly Property VerbType As String
    Friend ReadOnly Property VerbTypeName As String
    Friend Sub New(verbType As String, verbCategoryType As String, verbTypeName As String)
        Me.VerbType = verbType
        Me.VerbTypeName = verbTypeName
        Me.VerbCategoryType = verbCategoryType
    End Sub
    Friend MustOverride Function Perform(character As ICharacter) As IDialog
    Friend Overridable Function CanPerform(character As ICharacter) As Boolean
        Return Not character.IsDead
    End Function
End Class
