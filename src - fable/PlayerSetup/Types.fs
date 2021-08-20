namespace PlayerSetupImplementation

module Types =
    type Model =
        { NumberOfPlayers: int }
        with
            static member defaultValue () = { NumberOfPlayers = 0 }

    type Msg =
        | Change of string
        | Submit of int
