Imports System.Runtime.CompilerServices
Imports TGGD.Business

Friend Module LocationExtensions
    <Extension>
    Friend Function HandleBump(location As ILocation, character As ICharacter) As IDialog
        Return location.LocationType.ToLocationTypeDescriptor.OnBump(location, character)
    End Function
    <Extension>
    Friend Sub HandleLeave(location As ILocation, character As ICharacter)
        location.LocationType.ToLocationTypeDescriptor.OnLeave(location, character)
    End Sub
    <Extension>
    Friend Function HandleEnter(location As ILocation, character As ICharacter) As IDialog
        Return location.LocationType.ToLocationTypeDescriptor.OnEnter(location, character)
    End Function
End Module
