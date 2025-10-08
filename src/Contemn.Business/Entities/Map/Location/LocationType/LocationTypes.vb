Friend Module LocationTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, LocationTypeDescriptor) =
        New List(Of LocationTypeDescriptor) From
        {
            New GrassLocationTypeDescriptor(),
            New TreeLocationTypeDescriptor(),
            New WaterLocationTypeDescriptor(),
            New DirtLocationTypeDescriptor(),
            New RockLocationTypeDescriptor(),
            New CampFireLocationTypeDescriptor(),
            New KilnLocationTypeDescriptor(),
            New TutorialHouseLocationTypeDescriptor()
        }.ToDictionary(Function(x) x.LocationType, Function(x) x)
End Module
