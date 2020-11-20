module Player.StateManagement

open Elmish
open Player.Types

let private decrease minimum value =
    let newValue = value - 1
    if newValue < minimum
    then minimum
    else newValue

let private decreaseMin0 = decrease 0
let private decreaseMin1 = decrease 1

let init (name: string) : Model * Cmd<Msg> =
    { Name = ""
      Level = 1
      Bonuses = 0
      Curses = 0
      Total = 1
      IsOpaque = false
      IsWinner = false }
    , Cmd.ofMsg (ChangeName name)

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | ChangeName name ->
        ({ model with Name = name }, Cmd.none)
    | IncreaseLevel ->
        ({ model with Level = model.Level + 1 }, Cmd.none)
    | DecreaseLevel ->
        ({ model with Level = decreaseMin1 model.Level }, Cmd.none)
    | IncreaseBonuses ->
        ({ model with Bonuses = model.Bonuses + 1 }, Cmd.none)
    | DecreaseBonuses ->
        ({ model with Bonuses = decreaseMin0 model.Bonuses }, Cmd.none)
    | IncreaseCurses ->
        ({ model with Curses = model.Curses + 1 }, Cmd.none)
    | DecreaseCurses ->
        ({ model with Curses = decreaseMin0 model.Curses }, Cmd.none)

let view (model: Model) dispatch =
    Player.View.view model dispatch