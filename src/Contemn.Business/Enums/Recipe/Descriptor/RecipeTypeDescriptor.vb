Friend MustInherit Class RecipeTypeDescriptor
    Private ReadOnly inputs As IReadOnlyDictionary(Of String, Integer)
    Private ReadOnly outputs As IReadOnlyDictionary(Of String, Integer)
    Sub New(
           inputs As IReadOnlyDictionary(Of String, Integer),
           outputs As IReadOnlyDictionary(Of String, Integer))
        Me.inputs = inputs
        Me.outputs = outputs
    End Sub
    Friend Overridable Function CanCraft(character As ICharacter) As Boolean
        Return inputs.All(Function(x) character.GetCountOfItemType(x.Key) >= x.Value)
    End Function

    Friend Function HasInput(itemType As String) As Boolean
        Return inputs.ContainsKey(itemType)
    End Function
End Class
