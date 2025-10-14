Imports TGGD.UI
Imports Contemn.Business

Friend Module NeutralState
    Friend Function DetermineState(
                                  buffer As IUIBuffer(Of Integer),
                                  world As IWorld,
                                  playSfx As Action(Of String),
                                  settings As ISettings) As IUIState
        Dim avatar = world.Avatar
        If avatar.IsDead Then
            playSfx(Sfx.PlayerDeath)
            Return New DeadState(buffer, world, playSfx, settings)
        End If
        Return New NavigationState(buffer, world, playSfx, settings)
    End Function
End Module
