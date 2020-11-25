module App

open Elmish
open Elmish.Debug
open Elmish.React
open Fable.React
open Fable.Import
open Fable.React.Props
open Fable.React.Helpers

type AppState =
    { GameStarted: bool
      GameOver: bool
      PlayerSetup: PlayerSetup.Model
      Players: Player.Model list }

type Msg =
    | Setup of PlayerSetup.Msg
    | PlayerEvent of int * Player.Msg
    | EndGame
    | ResetGame

let init () =
    let (playerSetup, playerSetupCmd) = PlayerSetup.init()
    { GameStarted = false
      GameOver = false
      PlayerSetup = playerSetup
      Players = [] }
    , Cmd.map Setup playerSetupCmd

let updatePlayer (players: Player.Model list) (index: int) (newState: Player.Model) =
    players
    |> List.mapi (
        fun i player ->
            if i = index
            then newState
            else player
        )

let update (msg: Msg) (state: AppState) : AppState * Cmd<Msg> =
    match msg with
    | Setup setup ->
        let (res, cmd) = PlayerSetup.update setup state.PlayerSetup

        match setup with
        | PlayerSetup.Msg.Submit numberOfPlayers ->
            if res.NumberOfPlayers > 0 then
                let players = [
                    for i in 1 .. res.NumberOfPlayers ->
                        { Player.Model.defaultValue() with Name = sprintf "Player %i" i }
                ]
                ({ state with PlayerSetup = res
                              Players = players
                              GameStarted = true }
                , Cmd.none)
            else
                ({ state with PlayerSetup = res }
                , Cmd.none)

        | PlayerSetup.Msg.Change _ ->
            ({ state with PlayerSetup = res }, Cmd.none)

    | PlayerEvent (playerIndex, playerEvent) ->
        let (res, cmd) = Player.update playerEvent state.Players.[playerIndex]
        ({ state with Players = (updatePlayer state.Players playerIndex res) }, Cmd.none)

    | EndGame -> state, Cmd.none

    | ResetGame -> state, Cmd.none

// Couldn't figure out how to compose this function in a way that would allow partial application of the last parameter so I can use it to build the PlayerEvent message.
let playerDispatchWrapper (appDispatch: Msg -> unit) (playerIndex: int) (playerMessage: Player.Msg) : unit =
    appDispatch <| PlayerEvent (playerIndex, playerMessage)

let view (state: AppState) (dispatch: Msg -> unit) =
    if not state.GameStarted then
        PlayerSetup.view state.PlayerSetup (dispatch << Setup)
    else
        div
            [   ClassName "container-fluid" ]
            [   div
                    [   ClassName "row mt-4 mb-4" ]
                    [   for i in 0 .. (state.PlayerSetup.NumberOfPlayers - 1) ->
                            Player.view state.Players.[i] (playerDispatchWrapper dispatch i)
                    ]

                div
                    [   ClassName "row" ]
                    [   div
                            [   ClassName "col-md-12" ]
                            [
                                button
                                    [   ClassName "btn btn-danger"
                                        Type "button"
                                    ]
                                    [   str "Start new game"]
                            ]
                    ]
            ]

Program.mkProgram init update view
|> Program.withReactHydrate "app"
|> Program.withDebugger
|> Program.run