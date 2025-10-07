Public Interface IWorldCharacters
    Function CreateCharacter(characterType As String, location As ILocation) As ICharacter
    Function GetCharacter(characterId As Integer) As ICharacter
    Property Avatar As ICharacter
    Sub ActivateCharacter(character As ICharacter)
    Sub DeactivateCharacter(character As ICharacter)
    ReadOnly Property ActiveCharacters As IEnumerable(Of ICharacter)
End Interface
