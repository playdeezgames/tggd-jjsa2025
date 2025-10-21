Imports TGGD.Business

Public Class TutorialHouseLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(
            NameOf(TutorialHouseLocationTypeDescriptor),
            "Tutorial House")
    End Sub

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnInitialize(location As Location)
        location.SetTag(TagType.IsTutorialHouse, True)
    End Sub

    Friend Overrides Sub OnProcessTurn(location As Location)
        Throw New NotImplementedException
    End Sub

    Friend Overrides Sub CleanUp(location As Location)
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        Return BumpDialog.LaunchMenu(character).Invoke
    End Function

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return False
    End Function

    Friend Overrides Function Describe(location As Location) As IEnumerable(Of IDialogLine)
        Return {
            New DialogLine(MoodType.Info, "It's housy.")
            }
    End Function
End Class
