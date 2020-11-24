namespace PlayerImplementation

module Types =
    type Model =
        { Name: string
          Level: int
          Bonuses: int
          Curses: int
          Total: int
          IsOpaque: bool
          IsWinner: bool }
          with
            static member defaultValue() =
                { Name = ""
                  Level = 1
                  Bonuses = 0
                  Curses = 0
                  Total = 1
                  IsOpaque = false
                  IsWinner = false }

    type Msg =
        | IncreaseLevel
        | DecreaseLevel
        | IncreaseBonuses
        | DecreaseBonuses
        | IncreaseCurses
        | DecreaseCurses
        | ChangeName of string
