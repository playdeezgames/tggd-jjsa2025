Friend Class OverworldMapTypeDescriptor
    Inherits BaseMapTypeDescriptor

    Shared ReadOnly terrainGenerator As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {NameOf(TreeLocationTypeDescriptor), 100},
            {NameOf(WaterLocationTypeDescriptor), 25},
            {NameOf(RockLocationTypeDescriptor), 25},
            {NameOf(GrassLocationTypeDescriptor), 1000}
        }

    Public Sub New()
        MyBase.New(NameOf(OverworldMapTypeDescriptor), 1, terrainGenerator)
    End Sub
End Class
