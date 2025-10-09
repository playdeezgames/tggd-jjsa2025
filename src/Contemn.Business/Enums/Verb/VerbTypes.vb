Imports System.Runtime.CompilerServices

Friend Module VerbTypes
    Private Function GenerateDescriptors() As IList(Of VerbTypeDescriptor)
        Dim result = New List(Of VerbTypeDescriptor) From
        {
            New BoilVerbTypeDescriptor(),
            New ChopWoodVerbTypeDescriptor(),
            New CollectCarrotTutorialVerbTypeDescriptor(),
            New CollectFishTutorialVerbTypeDescriptor(),
            New CollectHammerTutorialVerbTypeDescriptor(),
            New CollectNetTutorialVerbTypeDescriptor(),
            New CollectPlantFiberTutorialVerbTypeDescriptor(),
            New CollectRockTutorialVerbTypeDescriptor(),
            New CollectStickTutorialVerbTypeDescriptor(),
            New CollectStickVerbTypeDescriptor(),
            New CollectTwineTutorialVerbTypeDescriptor(),
            New CookVerbTypeDescriptor(),
            New CraftVerbTypeDescriptor(),
            New DigClayVerbTypeDescriptor(),
            New DismantleVerbTypeDescriptor(),
            New DrinkVerbTypeDescriptor(),
            New ExtinguishVerbTypeDescriptor(),
            New FillVerbTypeDescriptor(),
            New FireVerbTypeDescriptor(),
            New FishVerbTypeDescriptor(),
            New ForageVerbTypeDescriptor(),
            New GroundVerbTypeDescriptor(),
            New InventoryVerbTypeDescriptor(),
            New LightTorchVerbTypeDescriptor(),
            New LightVerbTypeDescriptor(),
            New MoveEastVerbTypeDescriptor(),
            New MoveNorthVerbTypeDescriptor(),
            New MoveSouthVerbTypeDescriptor(),
            New MoveWestVerbTypeDescriptor(),
            New PlaceVerbTypeDescriptor(),
            New RecipediaVerbTypeDescriptor(),
            New RefuelVerbTypeDescriptor()
        }
        Return result
    End Function

    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, VerbTypeDescriptor) =
        GenerateDescriptors().ToDictionary(Function(x) x.VerbType, Function(x) x)
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    Friend Function AllOfCategory(verbCategoryType As String) As IEnumerable(Of String)
        Return Descriptors.Values.Where(Function(x) x.VerbCategoryType = verbCategoryType).Select(Function(x) x.VerbType)
    End Function
End Module
