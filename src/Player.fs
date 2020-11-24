module Player

type Model =
    {   Name: string
        Level: int
        Bonuses: int
        Curses: int
        Total: int
        IsOpaque: bool
        IsWinner: bool }

type Msg =
    | IncreaseLevel
    | DecreaseLevel
    | IncreaseBonuses
    | DecreaseBonuses
    | IncreaseCurses
    | DecreaseCurses
    | ChangeName of string

let defaultValue =
    {   Name = ""
        Level = 1
        Bonuses = 0
        Curses = 0
        Total = 1
        IsOpaque = false
        IsWinner = false }

let init (name: string) : Player * Cmd<PlayerMessages> =
    (defaultValue, Cmd.ofMsg (ChangeName name))

let update (msg: PlayerMessages) (model: Player) : Player * Cmd<PlayerMessages> =
    let decrease minimum value =
        let newValue = value - 1
        if newValue < minimum
        then minimum
        else newValue

    let decreaseMin0 = decrease 0
    let decreaseMin1 = decrease 1

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
