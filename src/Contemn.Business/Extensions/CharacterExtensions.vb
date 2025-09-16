Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module CharacterExtensions
    <Extension>
    Friend Function HandleBump(character As ICharacter, location As ILocation) As IDialog
        Return character.CharacterType.ToCharacterTypeDescriptor.OnBump(character, location)
    End Function
    <Extension>
    Friend Sub HandleLeave(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnLeave(character, location)
    End Sub
    <Extension>
    Friend Sub HandleEnter(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnEnter(character, location)
    End Sub

End Module
