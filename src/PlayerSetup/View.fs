namespace PlayerSetupImplementation

[<AutoOpen>]
module View =
    open PlayerSetupImplementation.Types
    open Fable.React.Props
    open Fable.React.Helpers
    open Fable.React.Standard
    open Fable.React

    let view (model: Model) dispatch =
        div
            [   ClassName "container-fluid" ]
            [   div
                    [   ClassName "row mt-4"]
                    [   div
                            [   ClassName "col-md-4 col-sm-6" ]
                            [   div
                                    [   ClassName "form-group"]
                                    [   label [] [ str "How many players are going to play?"]
                                        input [ ClassName "form-control"
                                                Type "text"
                                                Value model.NumberOfPlayers
                                                OnChange (fun e -> dispatch <| Change e.Value ) ]
                                    ]

                                button
                                    [   ClassName "btn btn-primary"
                                        Type "button"
                                        OnClick (fun _ -> dispatch <| Submit model.NumberOfPlayers)
                                    ]
                                    [   str "Start Game"
                                    ]
                            ]

                    ]

            ]