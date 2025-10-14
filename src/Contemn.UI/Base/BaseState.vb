Imports TGGD.UI
Imports Contemn.Business

Friend MustInherit Class BaseState
    Implements IUIState
    Sub New(
           buffer As IUIBuffer(Of Integer),
           world As IWorld,
           playSfx As Action(Of String),
           settings As ISettings)
        Me.Buffer = buffer
        Me.World = world
        Me.PlaySfx = playSfx
        Me.Settings = settings
    End Sub
    Protected ReadOnly Settings As ISettings
    Protected ReadOnly Property Buffer As IUIBuffer(Of Integer)
    Protected ReadOnly Property World As IWorld
    Protected ReadOnly Property PlaySfx As Action(Of String)
    Public MustOverride Sub Refresh() Implements IUIState.Refresh
    Public MustOverride Function HandleCommand(command As String) As IUIState Implements IUIState.HandleCommand
End Class
