Friend MustInherit Class RecipeTypeDescriptor
    Friend ReadOnly Property RecipeType As String
    Private ReadOnly inputs As IReadOnlyDictionary(Of String, Integer)
    Private ReadOnly outputs As IReadOnlyDictionary(Of String, Integer)
    Sub New(
           recipeType As String,
           inputs As IReadOnlyDictionary(Of String, Integer),
           outputs As IReadOnlyDictionary(Of String, Integer))
        Me.RecipeType = recipeType
        Me.inputs = inputs
        Me.outputs = outputs
    End Sub
    Friend Overridable Function CanCraft(character As ICharacter) As Boolean
        Return inputs.All(Function(x) character.GetCountOfItemType(x.Key) >= x.Value)
    End Function

    Friend Function Craft(character As ICharacter) As IEnumerable(Of String)
        Dim results As New List(Of String)
        Dim deltas As New Dictionary(Of String, Integer)
        For Each itemType In New HashSet(Of String)(inputs.Keys.Concat(outputs.Keys))
            deltas(itemType) = 0
            Dim count As Integer
            If inputs.TryGetValue(itemType, count) Then
                deltas(itemType) -= count
            End If
            If outputs.TryGetValue(itemType, count) Then
                deltas(itemType) += count
            End If
        Next
        For Each delta In deltas
            If delta.Value < 0 Then
                results.Add($"{delta.Value} {delta.Key.ToItemTypeDescriptor.ItemTypeName}")
                For Each item In character.ItemsOfType(delta.Key).Take(-delta.Value)
                    character.RemoveItem(item)
                    item.Recycle()
                Next
            ElseIf delta.Value > 0 Then
                results.Add($"+{delta.Value} {delta.Key.ToItemTypeDescriptor.ItemTypeName}")
                For Each dummy In Enumerable.Range(0, delta.Value)
                    character.World.CreateItem(delta.Key, character)
                Next
            End If
        Next
        Return results
    End Function

    Friend Function HasInput(itemType As String) As Boolean
        Return inputs.ContainsKey(itemType)
    End Function
    Friend ReadOnly Property Name As String
        Get
            Dim inputList = inputs.Select(Function(x) $"{x.Value} {x.Key.ToItemTypeDescriptor.ItemTypeName}")
            Dim outputList = outputs.Select(Function(x) $"{x.Value} {x.Key.ToItemTypeDescriptor.ItemTypeName}")
            Return $"{String.Join("+", inputList)}->{String.Join("+", outputList)}"
        End Get
    End Property
End Class
