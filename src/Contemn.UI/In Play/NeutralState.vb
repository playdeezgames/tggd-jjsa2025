Imports TGGD.UI
Imports Contemn.Business

Friend Module NeutralState
    Friend Function DetermineState(
                                  buffer As IUIBuffer(Of Integer),
                                  world As IWorld,
                                  settings As ISettings) As IUIState
        Dim avatar = world.Avatar
        If avatar.IsDead Then
            world.Platform.PlaySfx(Sfx.PlayerDeath)
            Return New DeadState(buffer, world, settings)
        End If
        If world.IsDemoComplete Then
            Return New DemoCompleteState(buffer, world, settings)
        End If
        If world.GetTag(TagType.ViewMode) Then
            Return New ViewState(buffer, world, settings)
        End If
        Return New NavigationState(buffer, world, settings)
    End Function
End Module
