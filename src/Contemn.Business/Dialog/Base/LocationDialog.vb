Imports TGGD.Business

Friend MustInherit Class LocationDialog
    Inherits BaseDialog

    Protected ReadOnly location As ILocation

    Sub New(
           location As ILocation,
           generateCaption As Func(Of ILocation, String),
           generateChoices As Func(Of ILocation, IEnumerable(Of IDialogChoice)),
           generateLines As Func(Of ILocation, IEnumerable(Of IDialogLine)),
           onCancelDialog As Func(Of IDialog))
        MyBase.New(
            generateCaption(location),
            generateChoices(location),
            generateLines(location),
            onCancelDialog)
        Me.location = location
    End Sub
End Class
