namespace PlayerImplementation

module Types =
    type Model =
        { Name: string
          Level: int
          Bonuses: int
          Curses: int
          Total: int
          IsDisabled: bool
          IsWinner: bool }
          with
            static member defaultValue() =
                { Name = ""
                  Level = 1
                  Bonuses = 0
                  Curses = 0
                  Total = 1
                  IsDisabled = false
                  IsWinner = false }

    type Msg =
        | UpdateState
        | IncreaseLevel
        | DecreaseLevel
        | IncreaseBonuses
        | DecreaseBonuses
        | IncreaseCurses
        | DecreaseCurses
        | ChangeName of string
