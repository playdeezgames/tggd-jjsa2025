Imports System.Runtime.CompilerServices

Friend Module GeneratorExtensions
    <Extension>
    Friend Function GenerateItem(Of TDescriptor)(generator As IGenerator, entity As IInventoryEntity(Of TDescriptor)) As IItem
        Dim itemType = generator.Generate()
        If Not String.IsNullOrEmpty(itemType) Then
            generator.SetWeight(itemType, generator.GetWeight(itemType) - 1)
            generator.SetWeight(String.Empty, generator.GetWeight(String.Empty) + 1)
            Return entity.World.CreateItem(itemType, entity)
        End If
        Return Nothing
    End Function
    <Extension>
    Friend Function IsDepleted(generator As IGenerator) As Boolean
        Return generator.GetWeight(String.Empty) = generator.TotalWeight
    End Function
End Module
