Friend MustInherit Class ForageTableTypeDescriptor
    Friend ReadOnly Property ForageTableType As String
    Sub New(forageTableType As String)
        Me.ForageTableType = forageTableType
    End Sub
    Friend MustOverride Sub Initialize(generator As IGenerator)
End Class
