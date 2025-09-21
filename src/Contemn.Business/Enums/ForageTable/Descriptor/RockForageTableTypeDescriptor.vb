Friend Class RockForageTableTypeDescriptor
    Inherits ForageTableTypeDescriptor

    Public Sub New()
        MyBase.New(Business.ForageTableType.Rock)
    End Sub

    Friend Overrides Sub Initialize(generator As IGenerator)
        generator.SetWeight(String.Empty, 5)
        generator.SetWeight(ItemType.Rock, 20)
    End Sub
End Class
