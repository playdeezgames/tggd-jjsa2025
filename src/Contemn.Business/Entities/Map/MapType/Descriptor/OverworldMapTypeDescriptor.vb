Friend Class OverworldMapTypeDescriptor
    Inherits BaseMapTypeDescriptor

    Shared ReadOnly terrainGenerator As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {LocationType.Dirt, 1}
        }

    Public Sub New()
        MyBase.New(NameOf(OverworldMapTypeDescriptor), 1, terrainGenerator)
    End Sub
End Class
