namespace PlayerImplementation

[<AutoOpen>]
module State =
    open Elmish
    open PlayerImplementation.Types

    let private decrease minimum value =
        let newValue = value - 1
        if newValue < minimum
        then minimum
        else newValue

    let private decreaseMin0 = decrease 0
    let private decreaseMin1 = decrease 1

    let private getTotal level bonuses curses : int =
        level + bonuses - curses

    let init (name: string) : Model * Cmd<Msg> =
        (Model.defaultValue(), Cmd.ofMsg (ChangeName name))

    let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
        match msg with
        | ChangeName name ->
            ({ model with Name = name }, Cmd.none)

        | IncreaseLevel ->
            let level = model.Level + 1
            ({ model with Level = level
                          Total = getTotal level model.Bonuses model.Curses }
            , Cmd.none)

        | DecreaseLevel ->
            let level = decreaseMin1 model.Level
            ({ model with Level = level
                          Total = getTotal level model.Bonuses model.Curses}
            , Cmd.none)

        | IncreaseBonuses ->
            let bonuses = model.Bonuses + 1
            ({ model with Bonuses = bonuses
                          Total = getTotal model.Level bonuses model.Curses }
            , Cmd.none)

        | DecreaseBonuses ->
            let bonuses = decreaseMin0 model.Bonuses
            ({ model with Bonuses = bonuses
                          Total = getTotal model.Level bonuses model.Curses }
            , Cmd.none)

        | IncreaseCurses ->
            let curses = model.Curses + 1
            ({ model with Curses = curses
                          Total = getTotal model.Level model.Bonuses curses }
            , Cmd.none)

        | DecreaseCurses ->
            let curses = decreaseMin0 model.Curses
            ({ model with Curses = curses
                          Total = getTotal model.Level model.Bonuses curses }
            , Cmd.none)

        | UpdateState ->
            (model, Cmd.none)
