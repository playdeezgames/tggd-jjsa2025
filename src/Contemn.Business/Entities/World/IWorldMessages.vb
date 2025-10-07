Public Interface IWorldMessages
    Sub AddMessage(mood As String, text As String)
    ReadOnly Property MessageCount As Integer
    Sub DismissMessage()
    Function GetMessage(line As Integer) As IMessage
End Interface
