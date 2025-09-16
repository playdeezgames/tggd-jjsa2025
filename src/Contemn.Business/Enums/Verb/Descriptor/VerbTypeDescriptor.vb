Imports TGGD.Business

Friend MustInherit Class VerbTypeDescriptor
    ReadOnly Property VerbType As String
    ReadOnly Property VerbTypeName As String
    Sub New(verbType As String, verbTypeName As String)
        Me.VerbType = verbType
        Me.VerbTypeName = verbTypeName
    End Sub
    MustOverride Function Perform(character As ICharacter) As IDialog
    MustOverride Function CanPerform(character As ICharacter) As Boolean
    Function CanChoose(character As Character) As Boolean
        Return Not String.IsNullOrEmpty(VerbTypeName) AndAlso CanPerform(character)
    End Function
End Class
