Imports TGGD.Business

Public Class FishItemTypeDescriptor
    Inherits ConsumableItemTypeDescriptor
    Shared ReadOnly SLAP_CHOICE As String = NameOf(SLAP_CHOICE)
    Const SLAP_TEXT = "Slap!"

    Public Sub New()
        MyBase.New(
            NameOf(FishItemTypeDescriptor),
            "Fish",
            0,
            True,
            (1, 1, 4),
            {TagType.CanRefuel},
            New Dictionary(Of String, Integer))
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As IItem)
        MyBase.HandleInitialize(item)
        item.SetStatistic(StatisticType.Satiety, 20)
        item.SetStatistic(StatisticType.Fuel, 0)
    End Sub

    Protected Overrides Function OtherChoice(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case SLAP_CHOICE
                Return New OkDialog(
                    "SMACK!",
                    {
                        New DialogLine(MoodType.Info, "You slap that fish!")
                    },
                    Function() Nothing)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Friend Overrides Function GetAvailableChoices(item As IItem, character As ICharacter) As IEnumerable(Of IDialogChoice)
        Dim result As New List(Of IDialogChoice)(MyBase.GetAvailableChoices(item, character)) From {
            New DialogChoice(SLAP_CHOICE, SLAP_TEXT)
        }
        Return result
    End Function

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As IItem) As String
        Return ItemTypeName
    End Function

    Friend Overrides Function Describe(item As IItem) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's a fish.")
        }
    End Function
End Class
