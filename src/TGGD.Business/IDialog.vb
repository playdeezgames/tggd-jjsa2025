Public Interface IDialog
    ReadOnly Property Caption As String
    ReadOnly Property Choices As IEnumerable(Of (Choice As String, Text As String))
    ReadOnly Property Lines As IEnumerable(Of String)
    Function Choose(choice As String) As IDialog
    Function CancelDialog() As IDialog
End Interface
