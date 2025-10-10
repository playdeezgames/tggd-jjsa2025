Imports System.Runtime.CompilerServices

Friend Module VerbTypes
    Private Function GenerateDescriptors() As IList(Of VerbTypeDescriptor)
        Dim result = New List(Of VerbTypeDescriptor) From
        {
            New BoilVerbTypeDescriptor(),
            New ChopWoodVerbTypeDescriptor(),
            New CollectStickVerbTypeDescriptor(),
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
        result.AddRange(GenerateCollectItemTypeDescriptors())
        Return result
    End Function

    Private Function GenerateCollectItemTypeDescriptors() As IEnumerable(Of VerbTypeDescriptor)
        Return {
            New CollectItemTypeTutorialVerbTypeDescriptor(
                "Craft Blade",
                NameOf(BladeItemTypeDescriptor),
                TagType.CompletedCollectBladeTutorial,
                {TagType.CompletedCollectSharpRockTutorial},
                {
                    "Gather Sharp Rock and Hammer",
                    "Press <ACTION>",
                    "Select ""Craft...""",
                    "Select Blade recipe",
                    "Come Back"
                },
                {
                    "Blade is used to Craft."
                }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Forage for Carrot",
            NameOf(CarrotItemTypeDescriptor),
            TagType.CompletedCollectCarrotTutorial,
            Array.Empty(Of String),
            {
                "Move onto Grass",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Carrot",
                "Come back"
            },
            {
                "You can eat carrots."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Catch Fish!",
                    NameOf(FishItemTypeDescriptor),
                    TagType.CompletedCollectFishTutorial,
                    {TagType.CompletedCollectNetTutorial},
                    {
                        "Craft a Net",
                        "Go to water",
                        "Select ""Fish""",
                        "Repeat until you have a Fish",
                        "Come Back"
                    },
                    {
                        "Fish is food, not friends.",
                        "Raw fish may cause food poisoning.",
                        "Proper preparation and cooking is key."
                    }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Craft Hammer",
            NameOf(HammerItemTypeDescriptor),
            TagType.CompletedCollectHammerTutorial,
            {
                TagType.CompletedCollectTwineTutorial,
                TagType.CompletedCollectRockTutorial,
                TagType.CompletedCollectStickTutorial
            },
            {
                "Gather Stick, Rock, Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Hammer recipe",
                "Come Back"
            },
            {
                "Hammer is used to Craft."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Craft Knife",
            NameOf(KnifeItemTypeDescriptor),
            TagType.CompletedCollectKnifeTutorial,
            {TagType.CompletedCollectBladeTutorial},
            {
                "Gather Blade, Stick, and Twine",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Knife recipe",
                "Come Back"
            },
            {
                "Knife is used to Craft."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Forage for Plant Fiber",
            NameOf(PlantFiberItemTypeDescriptor),
            TagType.CompletedCollectPlantFiberTutorial,
            Array.Empty(Of String),
            {
                "Move onto Grass",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Plant Fiber",
                "Come back"
            },
            {
                "Plant Fiber is used to Craft things."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Craft Filet",
            NameOf(RawFishFiletItemTypeDescriptor),
            TagType.CompletedCollectRawFishFiletTutorial,
            {TagType.CompletedCollectKnifeTutorial, TagType.CompletedCollectFishTutorial},
            {
                "Gather Knife and Fish",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Filet recipe",
                "Come Back"
            },
            {
                "Filet can be cooked."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Forage for Rock",
            NameOf(RockItemTypeDescriptor),
            TagType.CompletedCollectRockTutorial,
            Array.Empty(Of String),
            {
                "Move onto Rock",
                "Press <ACTION>",
                "Select ""Forage...""",
                "Repeat until you get some Rock",
                "Come back"
            },
            {
                "Rocks are used to Craft."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Craft Sharp Rock",
            NameOf(SharpRockItemTypeDescriptor),
            TagType.CompletedCollectSharpRockTutorial,
            {TagType.CompletedCollectHammerTutorial},
            {
                "Gather Rock and Hammer",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Sharp Rock recipe",
                "Come Back"
            },
            {
                "Sharp Rock is used to Craft."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Collect Stick",
            NameOf(StickItemTypeDescriptor),
            TagType.CompletedCollectStickTutorial,
            Array.Empty(Of String),
            {
                "Move into Tree",
                "Select ""Collect Stick""",
                "Come back"
            },
            {
                "Stick are used to Craft.",
                "They may be used as fuel."
            }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
            "Craft Twine",
            NameOf(TwineItemTypeDescriptor),
            TagType.CompletedCollectTwineTutorial,
            {TagType.CompletedCollectPlantFiberTutorial},
            {
                "Gather sufficient Plant Fiber",
                "Press <ACTION>",
                "Select ""Craft...""",
                "Select Twine recipe",
                "Come Back"
            },
            {
                "Twine is used to Craft."
            })
            }
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
