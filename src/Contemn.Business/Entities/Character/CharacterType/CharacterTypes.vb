Imports System.Runtime.CompilerServices

Module CharacterTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, CharacterTypeDescriptor) =
        New List(Of CharacterTypeDescriptor) From
        {
            New N00bCharacterTypeDescriptor()
        }.ToDictionary(Function(x) x.CharacterType, Function(x) x)
End Module
