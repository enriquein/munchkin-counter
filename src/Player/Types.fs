namespace Player.Types

type Model =
    { Name: string
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
