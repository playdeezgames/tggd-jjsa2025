Imports Contemn.Business
Imports TGGD.UI

Friend Class EmbarkationState
    Inherits PickerState

    Shared ReadOnly TUTORIAL_IDENTIFIER As String = NameOf(TUTORIAL_IDENTIFIER)
    Shared ReadOnly EASY_IDENTIFIER As String = NameOf(EASY_IDENTIFIER)
    Shared ReadOnly NORMAL_IDENTIFIER As String = NameOf(NORMAL_IDENTIFIER)
    Shared ReadOnly HARD_IDENTIFIER As String = NameOf(HARD_IDENTIFIER)
    Const TUTORIAL_TEXT = "Tutorial"
    Const EASY_TEXT = "Easy"
    Const NORMAL_TEXT = "Normal"
    Const HARD_TEXT = "Hard"

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  settings As ISettings)
        MyBase.New(
            buffer,
            world,
            settings,
            "Choose Difficulty",
            Hue.Magenta,
            {
                (GO_BACK_IDENTIFIER, GO_BACK_TEXT),
                (TUTORIAL_IDENTIFIER, TUTORIAL_TEXT),
                (EASY_IDENTIFIER, EASY_TEXT),
                (NORMAL_IDENTIFIER, NORMAL_TEXT),
                (HARD_IDENTIFIER, HARD_TEXT)
            })
    End Sub

    Protected Overrides Function HandleCancel() As IUIState
        Return New MainMenuState(Buffer, World, Settings)
    End Function

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Select Case identifier
            Case GO_BACK_IDENTIFIER
                Return HandleCancel()
            Case TUTORIAL_IDENTIFIER
                Return HandleTutorial()
            Case EASY_IDENTIFIER
                Return HandleEasy()
            Case NORMAL_IDENTIFIER
                Return HandleNormal()
            Case HARD_IDENTIFIER
                Return HandleHard()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function HandleHard() As IUIState
        World.PrepareAndInitialize(Sub(x) x.SetMetadata(MetadataType.Difficulty, HARD_DIFFICULTY))
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function

    Private Function HandleNormal() As IUIState
        World.PrepareAndInitialize(Sub(x) x.SetMetadata(MetadataType.Difficulty, NORMAL_DIFFICULTY))
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function

    Private Function HandleEasy() As IUIState
        World.PrepareAndInitialize(Sub(x) x.SetMetadata(MetadataType.Difficulty, EASY_DIFFICULTY))
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function

    Private Function HandleTutorial() As IUIState
        World.PrepareAndInitialize(Sub(x) x.SetMetadata(MetadataType.Difficulty, TUTORIAL_DIFFICULTY))
        Return NeutralState.DetermineState(Buffer, World, Settings)
    End Function
End Class
