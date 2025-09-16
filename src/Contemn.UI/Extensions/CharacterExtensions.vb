Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module CharacterExtensions
    Private ReadOnly characterPixelTable As IReadOnlyDictionary(Of String, Func(Of ICharacter, Integer)) =
        New Dictionary(Of String, Func(Of ICharacter, Integer)) From
        {
            {CharacterType.N00b, AddressOf N00bToPixel}
        }

    Private Function N00bToPixel(character As ICharacter) As Integer
        Return UIBufferExtensions.ToPixel(2, Hue.White, Hue.Black)
    End Function

    <Extension>
    Friend Function ToPixel(character As ICharacter) As Integer
        Return characterPixelTable(character.CharacterType)(character)
    End Function
End Module
