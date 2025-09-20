Imports TGGD.Business

Public Interface IItem
    Inherits IEntity
    ReadOnly Property ItemId As Integer
    ReadOnly Property ItemType As String
    ReadOnly Property Name As String
    Function GetAvailableChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
    Function MakeChoice(character As ICharacter, choice As String) As IDialog
    Function Describe() As IEnumerable(Of (Mood As String, Text As String))
End Interface
