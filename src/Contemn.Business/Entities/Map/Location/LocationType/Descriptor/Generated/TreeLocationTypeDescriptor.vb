Imports TGGD.Business

Friend Class TreeLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Tree, "Tree")
    End Sub

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnInitialize(location As Location)
        location.SetStatistic(StatisticType.Resource, 50)
        location.SetStatisticMinimum(StatisticType.Resource, 0)
        location.SetTag(TagType.IsChoppable, True)
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return New BumpDialog(character, Array.Empty(Of IDialogLine))
    End Function

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return False
    End Function
    Friend Shared Function DepleteTree(bumpLocation As ILocation) As IEnumerable(Of IDialogLine)
        Dim result As New List(Of IDialogLine)
        If bumpLocation.IsStatisticAtMinimum(StatisticType.Resource) Then
            result.Add(New DialogLine(MoodType.Warning, $"{bumpLocation.Name} depleted."))
            bumpLocation.LocationType = Business.LocationType.Dirt
        End If
        Return result
    End Function

    Friend Overrides Sub OnProcessTurn(location As Location)
        Throw New NotImplementedException()
    End Sub
End Class
