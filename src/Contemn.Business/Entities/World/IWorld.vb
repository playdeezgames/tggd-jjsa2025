
Imports TGGD.Business

Public Interface IWorld
    Inherits IEntity,
        IWorldMaps,
        IWorldLocations,
        IWorldCharacters,
        IWorldItems,
        IWorldMessages,
        IWorldGenerators
    Function ProcessTurn() As IEnumerable(Of IDialogLine)
    Sub PrepareAndInitialize(preparation As Action(Of IWorld))
End Interface
