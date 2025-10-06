Imports TGGD.Business

Public Class CampFireLocationTypeDescriptor
    Inherits LocationTypeDescriptor
    Const FUEL_MAXIMUM = 25
    Const FUEL_INITIAL = FUEL_MAXIMUM \ 3

    Public Sub New()
        MyBase.New(
            NameOf(CampFireLocationTypeDescriptor),
            "Camp Fire")
    End Sub

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnInitialize(location As Location)
        location.SetStatisticRange(StatisticType.Fuel, FUEL_INITIAL, 0, FUEL_MAXIMUM)
        location.SetTag(TagType.IsRefuelable, True)
        location.World.ActivateLocation(location)
        location.SetTag(TagType.CanCook, True)
        location.SetTag(TagType.CanBoil, True)
        location.SetMetadata(MetadataType.DismantledLocationType, NameOf(DirtLocationTypeDescriptor))
        location.SetTag(TagType.CanDismantle, True)
        location.SetDismantleTable(location.World.CreateGenerator(NameOf(DismantleCampFireGeneratorType)))
        location.SetTag(TagType.IsLightable, True)
    End Sub

    Friend Overrides Sub OnProcessTurn(location As Location)
        If location.GetTag(TagType.IsLit) Then
            location.ChangeStatistic(StatisticType.Fuel, -1)
            If location.IsStatisticAtMinimum(StatisticType.Fuel) Then
                location.SetTag(TagType.IsLit, False)
            End If
        End If
    End Sub

    Friend Overrides Sub CleanUp(location As Location)
        location.GetDismantleTable().Recycle()
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return New BumpDialog(character)
    End Function
    Friend Overrides Function GenerateBumpLines(location As Location, character As ICharacter) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, location.FormatStatistic(StatisticType.Fuel))
            }.AppendIf(location.GetTag(TagType.IsLit), New DialogLine(MoodType.Info, "Is burning."))
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
