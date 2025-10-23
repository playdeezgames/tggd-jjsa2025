Imports TGGD.Business

Friend MustInherit Class CharacterDialog
    Inherits BaseDialog

    Protected ReadOnly character As ICharacter

    Sub New(
           character As ICharacter,
           generateCaption As Func(Of ICharacter, String),
           generateChoices As Func(Of ICharacter, IEnumerable(Of IDialogChoice)),
           generateLines As Func(Of ICharacter, IEnumerable(Of IDialogLine)))
        MyBase.New(
            generateCaption(character),
            generateChoices(character),
            generateLines(character))
        Me.character = character
    End Sub
End Class
