Imports Contemn.Data
Imports TGGD.Business

Friend Class Generator
    Implements IGenerator
    Private ReadOnly data As WorldData
    Private ReadOnly Property GeneratorId As Integer Implements IGenerator.GeneratorId
    Private ReadOnly Property GeneratorData As Dictionary(Of String, Integer)
        Get
            Return data.Generators(generatorId)
        End Get
    End Property
    Public ReadOnly Property TotalWeight As Integer Implements IGenerator.TotalWeight
        Get
            Return GeneratorData.Values.Sum
        End Get
    End Property

    Public ReadOnly Property Keys As IEnumerable(Of String) Implements IGenerator.Keys
        Get
            Return GeneratorData.Keys
        End Get
    End Property

    Public ReadOnly Property IsDepleted As Boolean Implements IGenerator.IsDepleted
        Get
            Return GetWeight(String.Empty) = TotalWeight
        End Get
    End Property

    Public Sub New(data As WorldData, generatorId As Integer)
        Me.data = data
        Me.GeneratorId = generatorId
    End Sub
    Public Function Generate() As String Implements IGenerator.Generate
        Return RNG.FromGenerator(GeneratorData, Nothing)
    End Function
    Public Sub SetWeight(key As String, weight As Integer) Implements IGenerator.SetWeight
        If weight > 0 Then
            GeneratorData(key) = weight
        Else
            GeneratorData.Remove(key)
        End If
    End Sub
    Public Function GetWeight(key As String) As Integer Implements IGenerator.GetWeight
        Dim weight As Integer
        If GeneratorData.TryGetValue(key, weight) Then
            Return weight
        End If
        Return 0
    End Function

    Public Sub Recycle() Implements IGenerator.Recycle
        GeneratorData.Clear()
        data.RecycledGenerators.Add(GeneratorId)
    End Sub

    Public Function GenerateItem(Of TDescriptor)(entity As IInventoryEntity(Of TDescriptor)) As IItem Implements IGenerator.GenerateItem
        Dim generator = Me
        Dim itemType = Generator.Generate()
        If Not String.IsNullOrEmpty(itemType) Then
            Generator.SetWeight(itemType, Generator.GetWeight(itemType) - 1)
            Generator.SetWeight(String.Empty, Generator.GetWeight(String.Empty) + 1)
            Return entity.World.CreateItem(itemType, entity)
        End If
        Return Nothing
    End Function
End Class
