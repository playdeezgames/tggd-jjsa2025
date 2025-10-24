Imports TGGD.Business

Public Interface ICharacter
    Inherits IInventoryEntity(Of CharacterTypeDescriptor)
    ReadOnly Property CharacterType As String
    ReadOnly Property CharacterId As Integer
    ReadOnly Property IsAvatar As Boolean
    ReadOnly Property IsDead As Boolean

    ReadOnly Property Location As ILocation
    ReadOnly Property Map As IMap
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer

    Function Perform(verbType As String) As IDialog
    Function AvailableVerbsOfCategory(verbCategoryType As String) As IEnumerable(Of String)
    Function MoveTo(destination As ILocation) As IDialog
    Function ProcessTurn() As IEnumerable(Of IDialogLine)
    Sub Dismantle(item As IItem)
    ReadOnly Property Name As String
    ReadOnly Property HasAvailableRecipes As Boolean
    Sub SetBumpLocation(location As ILocation)
    Function GetBumpLocation() As ILocation
    Function CraftRecipe(
                        recipeType As String,
                        nextDialog As Func(Of IDialog),
                        confirmed As Boolean) As IDialog
    Function GetDurabilityTotal(tagType As String) As Integer
    Function ActionMenu() As IDialog
End Interface
