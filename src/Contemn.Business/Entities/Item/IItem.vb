Imports TGGD.Business

Public Interface IItem
    Inherits IDescribedEntity(Of ItemTypeDescriptor)
    ReadOnly Property ItemId As Integer
    ReadOnly Property ItemType As String
    ReadOnly Property Name As String
    ReadOnly Property CanDismantle As Boolean
    Function GetAvailableChoices(character As ICharacter) As IEnumerable(Of IDialogChoice)
    Function MakeChoice(character As ICharacter, choice As String) As IDialog
    Function Describe() As IEnumerable(Of IDialogLine)
    Function ProcessTurn() As IEnumerable(Of IDialogLine)
    Sub Place(location As ILocation)
    Function Deplete(character As ICharacter) As IEnumerable(Of IDialogLine)
End Interface
