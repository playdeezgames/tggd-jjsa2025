Imports Contemn.Data
Imports TGGD.Business

Public MustInherit Class DescribedEntity(Of TEntityData As EntityData, TDescriptor)
    Inherits Entity(Of TEntityData)
    Implements IDescribedEntity(Of TDescriptor)
    Protected Sub New(
                     data As WorldData,
                     playSfx As Action(Of String),
                     platform As IPlatform)
        MyBase.New(data, playSfx, platform)
    End Sub
    Public MustOverride ReadOnly Property Descriptor As TDescriptor Implements IDescribedEntity(Of TDescriptor).Descriptor
End Class
