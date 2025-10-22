Imports TGGD.Business

Public Interface ILocation
    Inherits IInventoryEntity(Of LocationTypeDescriptor)
    ReadOnly Property LocationId As Integer
    Property LocationType As String
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property HasCharacter As Boolean
    ReadOnly Property Character As ICharacter
    ReadOnly Property Name As String
    Sub ProcessTurn()
    Function GenerateBumpLines(character As ICharacter) As IEnumerable(Of IDialogLine)
    Function Describe() As IEnumerable(Of IDialogLine)
    Function NextLocation(direction As String) As ILocation
    Function GetDismantleTable() As IGenerator
    Sub SetDismantleTable(generator As IGenerator)
    Function GetForageGenerator() As IGenerator
End Interface
