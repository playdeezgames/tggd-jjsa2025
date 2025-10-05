Imports Contemn.Data

Public MustInherit Class DescribedEntity(Of TEntityData As EntityData, TDescriptor)
    Inherits Entity(Of TEntityData)
    Implements IDescribedEntity(Of TDescriptor)
    Protected Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub
    Public MustOverride ReadOnly Property Descriptor As TDescriptor Implements IDescribedEntity(Of TDescriptor).Descriptor
End Class
