Public Interface IUIContext
    ReadOnly Property [Event] As String
    Sub NextEvent()
    Sub Refresh()
    Sub HandleCommand(command As String)
End Interface
