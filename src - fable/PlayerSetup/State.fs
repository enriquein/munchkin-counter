namespace PlayerSetupImplementation

[<AutoOpen>]
module State =
    open System.Text.RegularExpressions
    open Elmish
    open PlayerSetupImplementation.Types

    let init () : Model * Cmd<Msg> =
        (Model.defaultValue(), Cmd.none)

    let private stripLetters (value: string) : string =
        let result = Regex.Replace(value, @"\D", "")
        if result = ""
        then "0"
        else result

    let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
        match msg with
        | Change name ->
            ({ model with NumberOfPlayers = int <| stripLetters name }, Cmd.none)

        | Submit numberOfPlayers ->
            ({ model with NumberOfPlayers = numberOfPlayers }, Cmd.none)
