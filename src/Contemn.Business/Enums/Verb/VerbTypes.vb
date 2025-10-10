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
                    {NameOf(SharpRockItemTypeDescriptor)},
                    {
                        "Gather Sharp Rock and Hammer",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Blade recipe",
                        "Come Back"
                    },
                    {"Blade is used to Craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Forage for Carrot",
                    NameOf(CarrotItemTypeDescriptor),
                    Array.Empty(Of String),
                    {
                        "Move onto Grass",
                        "Press <ACTION>",
                        "Select ""Forage...""",
                        "Repeat until you get some Carrot",
                        "Come back"
                    },
                    {"You can eat carrots."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Catch Fish!",
                    NameOf(FishItemTypeDescriptor),
                    {NameOf(FishingNetItemTypeDescriptor)},
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
                    {
                        NameOf(TwineItemTypeDescriptor),
                        NameOf(RockItemTypeDescriptor),
                        NameOf(StickItemTypeDescriptor)
                    },
                    {
                        "Gather Stick, Rock, Twine",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Hammer recipe",
                        "Come Back"
                    },
                    {"Hammer is used to Craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Knife",
                    NameOf(KnifeItemTypeDescriptor),
                    {NameOf(BladeItemTypeDescriptor)},
                    {
                        "Gather Blade, Stick, and Twine",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Knife recipe",
                        "Come Back"
                    },
                    {"Knife is used to Craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Forage for Plant Fiber",
                    NameOf(PlantFiberItemTypeDescriptor),
                    Array.Empty(Of String),
                    {
                        "Move onto Grass",
                        "Press <ACTION>",
                        "Select ""Forage...""",
                        "Repeat until you get some Plant Fiber",
                        "Come back"
                    },
                    {"Plant Fiber is used to Craft things."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Filet",
                    NameOf(RawFishFiletItemTypeDescriptor),
                    {NameOf(KnifeItemTypeDescriptor), NameOf(FishItemTypeDescriptor)},
                    {
                        "Gather Knife and Fish",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Filet recipe",
                        "Come Back"
                    },
                    {"Filet can be cooked."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Forage for Rock",
                    NameOf(RockItemTypeDescriptor),
                    Array.Empty(Of String),
                    {
                        "Move onto Rock",
                        "Press <ACTION>",
                        "Select ""Forage...""",
                        "Repeat until you get some Rock",
                        "Come back"
                    },
                    {"Rocks are used to Craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Sharp Rock",
                    NameOf(SharpRockItemTypeDescriptor),
                    {NameOf(HammerItemTypeDescriptor)},
                    {
                        "Gather Rock and Hammer",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Sharp Rock recipe",
                        "Come Back"
                    },
                    {"Sharp Rock is used to Craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Collect Stick",
                    NameOf(StickItemTypeDescriptor),
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
                    {NameOf(PlantFiberItemTypeDescriptor)},
                    {
                        "Gather sufficient Plant Fiber",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Twine recipe",
                        "Come Back"
                    },
                    {"Twine is used to Craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Axe",
                    NameOf(AxeItemTypeDescriptor),
                    {NameOf(SharpRockItemTypeDescriptor)},
                    {
                        "Gather Sharp Rock, Stick and Twine",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Axe recipe",
                        "Come Back"
                    },
                    {
                        "Axe is used to Craft.",
                        "Also good for chopping."
                    }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Camp Fire",
                    NameOf(CampFireItemTypeDescriptor),
                    {NameOf(RockItemTypeDescriptor), NameOf(StickItemTypeDescriptor)},
                    {
                        "Gather Sticks and Rocks",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Camp Fire recipe",
                        "Come Back"
                    },
                    {
                        "Camp Fire is placeable.",
                        "You can use it to cook stuff."
                    }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Dig Clay",
                    NameOf(ClayItemTypeDescriptor),
                    {NameOf(SharpStickItemTypeDescriptor)},
                    {
                        "Gather Sharp Stick",
                        "Go to water",
                        "Select Dig Clay",
                        "(repeat as needed)",
                        "Come Back"
                    },
                    {"Clay is used to craft."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Sharp Stick",
                    NameOf(SharpStickItemTypeDescriptor),
                    {NameOf(AxeItemTypeDescriptor)},
                    {
                        "Gather Axe and Stick",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Sharp Stick recipe",
                        "Come Back"
                    },
                    {"Sharp Stick is used to dig clay."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Torch",
                    NameOf(TorchItemTypeDescriptor),
                    {NameOf(StickItemTypeDescriptor), NameOf(PlantFiberItemTypeDescriptor)},
                    {
                        "Gather Stick and Fiber",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Torch recipe",
                        "Come Back"
                    },
                    {
                        "Torches are the most",
                        "useless things in this game."
                    }),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Cook Fish Filet",
                    NameOf(CookedFishFiletItemTypeDescriptor),
                    {
                        NameOf(CampFireItemTypeDescriptor),
                        NameOf(RawFishFiletItemTypeDescriptor),
                        NameOf(FireStarterItemTypeDescriptor)
                    },
                    {
                        "Gather Raw Filet",
                        "Place and light camp fire",
                        "Interact with camp fire",
                        "Select ""Cook...""",
                        "Select Raw Filet",
                        "Come Back"
                    },
                    {"Cooked fish filet are safe to eat."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Cook Charcoal",
                    NameOf(CharcoalItemTypeDescriptor),
                    {
                        NameOf(CampFireItemTypeDescriptor),
                        NameOf(LogItemTypeDescriptor),
                        NameOf(FireStarterItemTypeDescriptor)
                    },
                    {
                        "Gather Raw Filet",
                        "Place and light camp fire",
                        "Interact with camp fire",
                        "Select ""Cook...""",
                        "Select Log",
                        "Come Back"
                    },
                    {"Charcoal is the best fuel."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Unfired Pot",
                    NameOf(UnfiredPotItemTypeDescriptor),
                    {NameOf(ClayItemTypeDescriptor)},
                    {
                        "Gather Clay",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Unfired Pot recipe",
                        "Come Back"
                    },
                    {"Unfired pots can be fired in a kiln."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Kiln",
                    NameOf(KilnItemTypeDescriptor),
                    {NameOf(ClayItemTypeDescriptor), NameOf(RockItemTypeDescriptor)},
                    {
                        "Gather Clay and Rocks",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select Kiln recipe",
                        "Come Back"
                    },
                    {"A kiln can be used to fire a clay pot."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Chop Wood",
                    NameOf(LogItemTypeDescriptor),
                    {NameOf(ClayItemTypeDescriptor), NameOf(RockItemTypeDescriptor)},
                    {
                        "Make an Axe",
                        "Interact with a Tree",
                        "Select Chop Wood",
                        "Come Back"
                    },
                    {"Logs can fuel fires."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Fire Pot",
                    NameOf(FiredPotItemTypeDescriptor),
                    {
                        NameOf(KilnItemTypeDescriptor),
                        NameOf(FireStarterItemTypeDescriptor),
                        NameOf(UnfiredPotItemTypeDescriptor)
                    },
                    {
                        "Gather unfired pot",
                        "Place Kiln",
                        "Fuel and Light Kiln",
                        "Interact with kiln",
                        "Select Fire Pot",
                        "Come Back"
                    },
                    {"Clay pots can hold water."}),
                New CollectItemTypeTutorialVerbTypeDescriptor(
                    "Craft Firestarter",
                    NameOf(FireStarterItemTypeDescriptor),
                    {
                        NameOf(StickItemTypeDescriptor),
                        NameOf(PlantFiberItemTypeDescriptor)
                    },
                    {
                        "Gather sticks and plant fiber",
                        "Press <ACTION>",
                        "Select ""Craft...""",
                        "Select fire starter recipe",
                        "Come Back"
                    },
                    {"Fire starters can start fires."})
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
