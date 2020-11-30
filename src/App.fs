module App

open Elmish
open Elmish.Debug
open Elmish.React
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.React
open Fable.React.Props

// Imports for webpack
importSideEffects "primereact/resources/themes/saga-blue/theme.css"
importSideEffects "primereact/resources/primereact.min.css"
importSideEffects "primeicons/primeicons.css"
importSideEffects "primeflex/primeflex.css"

// Initialize PrimeReact
type IPrimeReact =
    abstract ripple: bool with get, set

[<ImportDefault("primereact/utils")>]
let PrimeReact: IPrimeReact = jsNative
PrimeReact.ripple <- true

type AppState =
    { GameStarted: bool
      GameOver: bool
      PlayerSetup: PlayerSetup.Model
      Players: Player.Model list }

type Msg =
    | Setup of PlayerSetup.Msg
    | PlayerEvent of int * Player.Msg
    | EndGame
    | UndoEndGame
    | ResetGameRequest
    | ResetGame

let init () =
    let (playerSetup, playerSetupCmd) = PlayerSetup.init()

    ({ GameStarted = false
       GameOver = false
       PlayerSetup = playerSetup
       Players = [] }
    , Cmd.map Setup playerSetupCmd)

let updatePlayer (players: Player.Model list) (index: int) (newState: Player.Model) =
    players
    |> List.mapi (
        fun i player ->
            if i = index
            then newState
            else player
        )

let updatePlayerWinnerAndOpaqueValues (players: Player.Model list) =
    players
    |> List.map (fun player ->
                    { player with IsWinner = (player.Level >= 10)
                                  IsDisabled = (player.Level < 10) }
                )

let partialTuple first second = (first, second)

let update (msg: Msg) (state: AppState) : AppState * Cmd<Msg> =
    match msg with
    | Setup (PlayerSetup.Msg.Submit numberOfPlayers) ->
        let (updatedModel, cmd) = PlayerSetup.update (PlayerSetup.Msg.Submit numberOfPlayers) state.PlayerSetup

        if numberOfPlayers > 0 then
            let players = [
                for i in 1 .. updatedModel.NumberOfPlayers ->
                    { Player.Model.defaultValue() with Name = sprintf "Player %i" i }
            ]
            ({ state with PlayerSetup = updatedModel
                          Players = players
                          GameStarted = true }
            , Cmd.none)
        else
            ({ state with PlayerSetup = updatedModel }, Cmd.none)

    | Setup (PlayerSetup.Msg.Change text) ->
        let (updatedModel, cmd) = PlayerSetup.update (PlayerSetup.Msg.Change text) state.PlayerSetup
        ({ state with PlayerSetup = updatedModel }, Cmd.none)

    | PlayerEvent (playerIndex, playerEvent) ->
        let (updatedModel, cmd) = Player.update playerEvent state.Players.[playerIndex]

        let extraCmd =
            if updatedModel.Level >= 10 then
                Cmd.ofMsg EndGame
            else
                if updatedModel.IsWinner && updatedModel.Level < 10 then
                    Cmd.ofMsg UndoEndGame
                else
                    Cmd.none

        ({ state with Players = (updatePlayer state.Players playerIndex updatedModel) }
        , Cmd.batch [ Cmd.map (PlayerEvent << (partialTuple playerIndex)) cmd; extraCmd ])

    | EndGame ->
        let players =
            state.Players
            |> List.map (fun player ->
                            { player with IsWinner = (player.Level >= 10)
                                          IsDisabled = (player.Level < 10) }
                        )

        ({ state with GameOver = true
                      Players = players }
        , Cmd.none)

    | UndoEndGame ->
        let stillHaveWinner = state.Players |> List.exists (fun player -> player.Level >= 10)
        let players =
            state.Players
            |> List.map (fun player ->
                            { player with IsWinner = (player.Level >= 10)
                                          IsDisabled = (stillHaveWinner && player.Level < 10) }
                        )

        ({ state with GameOver = false
                      Players = players }
        , Cmd.none)

    | ResetGameRequest ->
        let answer = Browser.Dom.window.prompt "Do you want to start a new game? (Yes or No)"
        let message =
            if answer.ToLowerInvariant() = "yes" || answer.ToLowerInvariant() = "y"
            then Cmd.ofMsg ResetGame
            else Cmd.none

        (state, message)


    | ResetGame ->
        let initialState, _ = init()
        (initialState, Cmd.none)

// Couldn't figure out how to compose this function in a way that would allow partial application of the last parameter so I can use it to build the PlayerEvent message.
let playerDispatchWrapper (appDispatch: Msg -> unit) (playerIndex: int) (playerMessage: Player.Msg) : unit =
    appDispatch <| PlayerEvent (playerIndex, playerMessage)

let view (state: AppState) (dispatch: Msg -> unit) =
    if not state.GameStarted then
        PlayerSetup.view state.PlayerSetup (dispatch << Setup)
    else
        div
            [   (*ClassName "csontainer-fluid"*)  ]
            [   div
                    [   HTMLAttr.Custom ("className", "p-grid") ]
                    [   for i in 0 .. (state.PlayerSetup.NumberOfPlayers - 1) ->
                            Player.view state.Players.[i] (dispatch << PlayerEvent << (partialTuple i))
                    ]

                // div
                //     [   ClassName "row" ]
                //     [   div
                //             [   ClassName "col-md-12" ]
                //             [
                //                 button
                //                     [   ClassName "btn btn-danger"
                //                         Type "button"
                //                         OnClick (fun _ -> dispatch ResetGameRequest)
                //                     ]
                //                     [   str "Start new game"]
                //             ]
                //     ]

                // div
                //     [   ClassName "row top-margin" ]
                //     [   div
                //             [ ClassName "col-md-12" ]
                //             []
                //     ]
            ]

Program.mkProgram init update view
|> Program.withReactHydrate "app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run