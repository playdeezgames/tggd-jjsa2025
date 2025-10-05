Public Interface IDescribedEntity(Of TDescriptor)
    Inherits IEntity
    ReadOnly Property Descriptor As TDescriptor
End Interface
