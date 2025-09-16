Public Interface IUIContext
    ReadOnly Property Sfx As String
    Sub NextSfx()
    Sub Refresh()
    Sub HandleCommand(command As String)
End Interface
