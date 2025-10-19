Public Interface IKeyBindings
    ReadOnly Property Commands As IEnumerable(Of String)
    Function BoundKeys(command As String) As IEnumerable(Of String)
    ReadOnly Property UnboundKeys As IEnumerable(Of String)
    Sub Unbind(key As String)
    Sub Bind(command As String, identifier As String)
    Sub Update()
End Interface
