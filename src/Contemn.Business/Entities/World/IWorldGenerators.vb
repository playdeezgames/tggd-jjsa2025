Public Interface IWorldGenerators
    Function CreateGenerator(generatorType As String) As IGenerator
    Function GetGenerator(generatorId As Integer) As IGenerator
End Interface
