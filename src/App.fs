module App

open Elmish
open Elmish.Debug
open Elmish.React
open Fable.React
open Fable.Import

type AppState =
    { GameStarted: bool
      GameOver: bool
      PlayerSetup: PlayerSetup.Model
      Players: Player.Model list }

type Msg =
    | Setup of PlayerSetup.Msg
    | EndGame
    | ResetGame

let init () =
    let (playerSetup, playerSetupCmd) = PlayerSetup.init()
    { GameStarted = false
      GameOver = false
      PlayerSetup = playerSetup
      Players = [] }
    , Cmd.map Setup playerSetupCmd

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
                , Cmd.map Setup cmd)
            else
                ({ state with PlayerSetup = res }
                , Cmd.map Setup cmd)
        | PlayerSetup.Msg.Change value ->
            ({ state with PlayerSetup = res }, Cmd.map Setup cmd)

    | EndGame -> state, Cmd.none

    | ResetGame -> state, Cmd.none

let view (state: AppState) (dispatch: Msg -> unit) =
    if not state.GameStarted then
        PlayerSetup.view state.PlayerSetup (dispatch << Setup)
    else div [] []

Program.mkProgram init update view
|> Program.withReactHydrate "app"
|> Program.withDebugger
|> Program.run