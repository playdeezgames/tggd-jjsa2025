Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module CharacterExtensions
    Private ReadOnly characterPixelTable As IReadOnlyDictionary(Of String, Func(Of ICharacter, Boolean, Integer)) =
        New Dictionary(Of String, Func(Of ICharacter, Boolean, Integer)) From
        {
            {NameOf(N00bCharacterTypeDescriptor), AddressOf N00bToPixel}
        }

    Private Function N00bToPixel(character As ICharacter, invert As Boolean) As Integer
        Return UIBufferExtensions.ToPixel(2, Hue.White, Hue.Black, invert)
    End Function

    <Extension>
    Friend Function ToPixel(character As ICharacter, invert As Boolean) As Integer
        Return characterPixelTable(character.CharacterType)(character, invert)
    End Function
    <Extension>
    Friend Function IsDead(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.Health) = character.GetStatisticMinimum(StatisticType.Health)
    End Function
End Module
