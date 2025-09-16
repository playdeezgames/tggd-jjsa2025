Imports Contemn.Data

Public Class Item
    Inherits Entity(Of ItemData)
    Implements IItem

    Public Sub New(
                  data As WorldData,
                  itemId As Integer,
                  playSfx As Action(Of String))
        MyBase.New(data, playSfx)
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
            Return ItemType.ToItemTypeDescriptor.GetName(Me)
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As ItemData
        Get
            Return Data.Items(ItemId)
        End Get
    End Property

    Public Overrides Sub Recycle()
        Clear()
        Data.RecycledItems.Add(ItemId)
    End Sub

    Public Function GetAvailableChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String)) Implements IItem.GetAvailableChoices
        Return ItemType.ToItemTypeDescriptor.GetAvailableChoices(Me, character)
    End Function

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        ItemType.ToItemTypeDescriptor.HandleInitialize(Me)
    End Sub
End Class
