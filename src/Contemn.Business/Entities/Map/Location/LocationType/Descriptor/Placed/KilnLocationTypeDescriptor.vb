Imports TGGD.Business

Public Class KilnLocationTypeDescriptor
    Inherits LocationTypeDescriptor
    Const FUEL_MAXIMUM = 50
    Public Sub New()
        MyBase.New(
            NameOf(KilnLocationTypeDescriptor),
            "Kiln")
    End Sub

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnInitialize(location As Location)
        location.SetStatisticRange(StatisticType.Fuel, 0, 0, FUEL_MAXIMUM)
        location.SetTag(TagType.IsRefuelable, True)
        location.World.ActivateLocation(location)
        location.SetTag(TagType.IsKiln, True)
        location.SetMetadata(MetadataType.DismantledLocationType, NameOf(DirtLocationTypeDescriptor))
        location.SetTag(TagType.CanDismantle, True)
        location.SetDismantleTable(location.World.CreateGenerator(NameOf(DismantleKilnGeneratorType)))
    End Sub

    Friend Overrides Sub OnProcessTurn(location As Location)
        If Not location.IsStatisticAtMinimum(StatisticType.Fuel) Then
            location.ChangeStatistic(StatisticType.Fuel, -1)
        End If
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return New BumpDialog(character)
    End Function

    Friend Overrides Function GenerateBumpLines(location As Location, character As ICharacter) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, location.FormatStatistic(StatisticType.Fuel))
            }
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
End Class
