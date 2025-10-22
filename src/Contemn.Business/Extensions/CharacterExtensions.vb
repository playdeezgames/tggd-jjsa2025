Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module CharacterExtensions
    <Extension>
    Friend Function GetDurabilityTotal(
                                      character As ICharacter,
                                      tag As String) As Integer
        Return character.Items.Where(Function(x) x.GetTag(tag)).Sum(Function(x) x.GetStatistic(StatisticType.Durability))
    End Function
End Module
