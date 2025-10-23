Public Module TagType
    Friend Function tagTypeName(tag As String) As String
        Select Case tag
            Case TagType.CanChop
                Return "(chopping item)"
            Case TagType.CanCut
                Return "(cutting item)"
            Case TagType.CanDig
                Return "(digging item)"
            Case TagType.CanHammer
                Return "(hammering item)"
            Case TagType.CanSharpen
                Return "(sharpening item)"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Friend Function CompletedCollectTutorial(itemType As String) As String
        Return $"CompletedCollect{itemType}Tutorial"
    End Function
    Friend ReadOnly CanBoil As String = NameOf(CanBoil)
    Friend ReadOnly CanChop As String = NameOf(CanChop)
    Friend ReadOnly CanCook As String = NameOf(CanCook)
    Friend ReadOnly CanCut As String = NameOf(CanCut)
    Friend ReadOnly CanDig As String = NameOf(CanDig)
    Friend ReadOnly CanDismantle As String = NameOf(CanDismantle)
    Friend ReadOnly CanFill As String = NameOf(CanFill)
    Friend ReadOnly CanFish As String = NameOf(CanFish)
    Friend ReadOnly CanHammer As String = NameOf(CanHammer)
    Friend ReadOnly CanLight As String = NameOf(CanLight)
    Friend ReadOnly CanKiln As String = NameOf(CanKiln)
    Friend ReadOnly CanPlace As String = NameOf(CanPlace)
    Friend ReadOnly CanRecover As String = NameOf(CanRecover)
    Friend ReadOnly CanRefuel As String = NameOf(CanRefuel)
    Friend ReadOnly CanSharpen As String = NameOf(CanSharpen)
    Friend ReadOnly IsBoilable As String = NameOf(IsBoilable)
    Friend ReadOnly IsChoppable As String = NameOf(IsChoppable)
    Friend ReadOnly IsCookable As String = NameOf(IsCookable)
    Friend ReadOnly IsDiggable As String = NameOf(IsDiggable)
    Friend ReadOnly IsFillable As String = NameOf(IsFillable)
    Friend ReadOnly IsFishable As String = NameOf(IsFishable)
    Friend ReadOnly IsIgnitable As String = NameOf(IsIgnitable)
    Friend ReadOnly IsKiln As String = NameOf(IsKiln)
    Public ReadOnly IsLit As String = NameOf(IsLit)
    Friend ReadOnly IsPlaceable As String = NameOf(IsPlaceable)
    Friend ReadOnly IsRefuelable As String = NameOf(IsRefuelable)
    Friend ReadOnly IsTutorialHouse As String = NameOf(IsTutorialHouse)
    Public ReadOnly Safe As String = NameOf(Safe)
    Public ReadOnly ViewMode As String = NameOf(ViewMode)
End Module
