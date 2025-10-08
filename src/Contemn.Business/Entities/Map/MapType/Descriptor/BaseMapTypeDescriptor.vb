Imports TGGD.Business

Friend MustInherit Class BaseMapTypeDescriptor
    Inherits MapTypeDescriptor

    Private ReadOnly terrainGenerator As IReadOnlyDictionary(Of String, Integer)

    Public Sub New(mapType As String, mapCount As Integer, terrainGenerator As IReadOnlyDictionary(Of String, Integer))
        MyBase.New(mapType, mapCount)
        Me.terrainGenerator = terrainGenerator
    End Sub

    Friend Overrides Sub OnInitialize(map As IMap)
        map.Columns = RoomColumns(map.World.GetMetadata(MetadataType.Difficulty))
        map.Rows = RoomRows(map.World.GetMetadata(MetadataType.Difficulty))
        For Each column In Enumerable.Range(0, map.Columns)
            For Each row In Enumerable.Range(0, map.Rows)
                Dim locationType = RNG.FromGenerator(terrainGenerator)
                map.World.CreateLocation(locationType, map, column, row)
            Next
        Next
    End Sub

    Private Shared ReadOnly RoomColumns As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {TUTORIAL_DIFFICULTY, 23},
            {EASY_DIFFICULTY, 19},
            {NORMAL_DIFFICULTY, 17},
            {HARD_DIFFICULTY, 13}
        }

    Private Shared ReadOnly RoomRows As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {TUTORIAL_DIFFICULTY, 23},
            {EASY_DIFFICULTY, 19},
            {NORMAL_DIFFICULTY, 17},
            {HARD_DIFFICULTY, 13}
        }
End Class
