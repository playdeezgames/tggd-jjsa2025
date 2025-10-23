
Imports TGGD.Business

Public Interface IWorld
    Inherits IEntity,
        IWorldMaps,
        IWorldLocations,
        IWorldCharacters,
        IWorldItems,
        IWorldMessages,
        IWorldGenerators

    ReadOnly Property IsDemoComplete As Boolean
    Function ProcessTurn() As IEnumerable(Of IDialogLine)
    Sub PrepareAndInitialize(preparation As Action(Of IWorld))
    Sub Save()
End Interface
