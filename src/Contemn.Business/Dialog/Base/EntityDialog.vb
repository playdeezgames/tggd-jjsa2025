Imports TGGD.Business

Friend MustInherit Class EntityDialog(Of TEntity As IEntity)
    Inherits BaseDialog

    Protected ReadOnly entity As TEntity

    Sub New(
           entity As TEntity,
           generateCaption As Func(Of TEntity, String),
           generateChoices As Func(Of TEntity, IEnumerable(Of IDialogChoice)),
           generateLines As Func(Of TEntity, IEnumerable(Of IDialogLine)),
           onCancelDialog As Func(Of IDialog))
        MyBase.New(
            generateCaption(entity),
            generateChoices(entity),
            generateLines(entity),
            onCancelDialog)
        Me.entity = entity
    End Sub
End Class
