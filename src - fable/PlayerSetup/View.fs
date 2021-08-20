namespace PlayerSetupImplementation

[<AutoOpen>]
module View =
    open PlayerSetupImplementation.Types
    open Browser.Types
    open Fable.React
    open Fable.React.Props
    open Prime

    let view (model: Model) dispatch =
        div
            [   primeClasses "p-grid"
                Style [ MarginTop "15px"; MarginLeft "15px" ]
            ]
            [   div
                    [   primeClasses "p-col-4 p-col-sm-6" ]
                    [   div
                            [   primeClasses "p-field p-component"]
                            [   label
                                    [   primeClasses "p-d-block" ]
                                    [   str "How many players are going to play?" ]
                                primeTextBox
                                    {|  className = "p-d-block"
                                        value = model.NumberOfPlayers
                                        onChange = (fun (e: Event) -> dispatch <| Change e.Value )
                                    |}
                            ]

                        primeButton
                            {| onClick = (fun _ -> dispatch <| Submit model.NumberOfPlayers)
                               label = "Start Game"
                            |}
                    ]
            ]
