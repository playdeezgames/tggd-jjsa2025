Imports TGGD.Business

Public Class GrassLocationTypeDescriptor
    Inherits LocationTypeDescriptor
    Public Sub New()
        MyBase.New(NameOf(GrassLocationTypeDescriptor), "Grass")
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return Nothing
    End Function

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
    End Sub

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Return Nothing
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return True
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return True
    End Function

    Friend Overrides Sub OnInitialize(location As Location)
        location.SetMetadata(MetadataType.ForageTable, NameOf(GrassForageGeneratorTypeDescriptor))
        location.SetTag(TagType.IsPlaceable, True)
    End Sub

    Friend Overrides Sub OnProcessTurn(location As Location)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub CleanUp(location As Location)
        location.GetForageGenerator()?.Recycle()
    End Sub

    Friend Overrides Function Describe(location As Location) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's grassy.")
            }
    End Function
End Class
