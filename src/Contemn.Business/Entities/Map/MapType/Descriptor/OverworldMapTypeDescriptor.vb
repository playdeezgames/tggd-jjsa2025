Friend Class OverworldMapTypeDescriptor
    Inherits BaseMapTypeDescriptor

    Shared ReadOnly terrainGenerator As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {LocationType.Tree, 100},
            {LocationType.Water, 25},
            {LocationType.Rock, 25},
            {LocationType.Grass, 1000}
        }

    Public Sub New()
        MyBase.New(NameOf(OverworldMapTypeDescriptor), 1, terrainGenerator)
    End Sub
End Class
