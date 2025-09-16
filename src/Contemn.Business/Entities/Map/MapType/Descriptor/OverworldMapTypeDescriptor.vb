Friend Class OverworldMapTypeDescriptor
    Inherits BaseMapTypeDescriptor

    Shared ReadOnly terrainGenerator As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {LocationType.Tree, 1},
            {LocationType.Grass, 10}
        }

    Public Sub New()
        MyBase.New(Business.MapType.Overworld, 1, terrainGenerator)
    End Sub
End Class
