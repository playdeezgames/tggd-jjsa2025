Imports Contemn.Data
Imports TGGD.Business

Friend Class Item
    Inherits Entity(Of ItemData)
    Implements IItem

    Public Sub New(
                  data As WorldData,
                  itemId As Integer,
                  platform As IPlatform)
        MyBase.New(data, platform)
        Me.ItemId = itemId
    End Sub

    Public ReadOnly Property ItemId As Integer Implements IItem.ItemId

    Public ReadOnly Property ItemType As String Implements IItem.ItemType
        Get
            Return EntityData.ItemType
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IItem.Name
        Get
            Return Descriptor.GetName(Me)
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As ItemData
        Get
            Return Data.Items(ItemId)
        End Get
    End Property

    Public ReadOnly Property Descriptor As ItemTypeDescriptor Implements IDescribedEntity(Of ItemTypeDescriptor).Descriptor
        Get
            Return ItemTypes.Descriptors(ItemType)
        End Get
    End Property

    Public ReadOnly Property CanDismantle As Boolean Implements IItem.CanDismantle
        Get
            Return Descriptor.DepletionTable.Any
        End Get
    End Property

    Public Overrides Sub Recycle()
        Data.ActiveItems.Remove(ItemId)
        Clear()
        Data.RecycledItems.Add(ItemId)
    End Sub

    Public Function GetAvailableChoices(character As ICharacter) As IEnumerable(Of IDialogChoice) Implements IItem.GetAvailableChoices
        Return Descriptor.GetAvailableChoices(Me, character)
    End Function

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        Descriptor.HandleInitialize(Me)
    End Sub

    Public Function MakeChoice(character As ICharacter, choice As String) As IDialog Implements IItem.MakeChoice
        Return Descriptor.Choose(Me, character, choice)
    End Function

    Public Function Describe() As IEnumerable(Of IDialogLine) Implements IItem.Describe
        Return Descriptor.Describe(Me)
    End Function

    Public Overrides Sub Clear()
        MyBase.Clear()
        EntityData.ItemType = Nothing
    End Sub

    Public Function ProcessTurn() As IEnumerable(Of IDialogLine) Implements IItem.ProcessTurn
        Return Descriptor.OnProcessTurn(Me)
    End Function

    Public Sub Place(location As ILocation) Implements IItem.Place
        location.LocationType = GetMetadata(MetadataType.PlaceLocationType)
    End Sub

    Public Function Deplete(character As ICharacter) As IEnumerable(Of IDialogLine) Implements IItem.Deplete
        Dim tool = Me
        Dim lines As New List(Of IDialogLine)
        If Not tool.HasStatistic(StatisticType.Durability) Then
            Return lines
        End If
        tool.ChangeStatistic(StatisticType.Durability, -1)
        Dim itemRuined = tool.IsStatisticAtMinimum(StatisticType.Durability)
        If itemRuined Then
            lines.Add(
            New DialogLine(
                MoodType.Info,
                $"Yer {tool.Name} is ruined!"))
            For Each entry In tool.Descriptor.DepletionTable
                Dim entryDescriptor = ItemTypes.Descriptors(entry.Key)
                lines.Add(New DialogLine(MoodType.Info, $"+{entry.Value} {entryDescriptor.ItemTypeName}"))
                For Each dummy In Enumerable.Range(0, entry.Value)
                    tool.World.CreateItem(entry.Key, character)
                Next
            Next
            character.RemoveAndRecycleItem(tool)
        Else
            lines.Add(
                New DialogLine(
                    MoodType.Info,
                    $"-1 {StatisticTypes.Descriptors(StatisticType.Durability).StatisticTypeName} {tool.Name}"))
            If tool.GetStatistic(StatisticType.Durability) < tool.GetStatisticMaximum(StatisticType.Durability) \ 3 Then
                lines.Add(
                New DialogLine(
                    MoodType.Warning,
                    $"Repair yer {tool.Name} soon."))
            End If
        End If
        Return lines
    End Function
End Class
