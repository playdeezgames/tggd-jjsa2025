Friend Class OverworldMapTypeDescriptor
    Inherits BaseMapTypeDescriptor

    Shared ReadOnly terrainGenerator As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {LocationType.Tree, 10},
            {LocationType.Water, 5},
            {LocationType.Grass, 100}
        }

    Public Sub New()
        MyBase.New(Business.MapType.Overworld, 1, terrainGenerator)
    End Sub
End Class
