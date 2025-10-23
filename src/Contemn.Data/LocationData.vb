Public Class LocationData
    Inherits InventoryEntityData
    Public Property LocationType As String
    Public Property MapId As Integer
    Public Property Column As Integer
    Public Property Row As Integer
    Public Property CharacterId As Integer?
    Public Property DismantleGeneratorId As Integer?
    Public Property ForageGeneratorId As Integer?
    Public Property Visible As Boolean
End Class
