module PlayerSetup

open Fable.React
open Elmish
open PlayerSetupImplementation.View
open PlayerSetupImplementation.State

type Model = PlayerSetupImplementation.Types.Model
type Msg = PlayerSetupImplementation.Types.Msg

let init () : Model * Cmd<Msg> =
    init()

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    update msg model

let view (model: Model) dispatch : ReactElement =
    view model dispatch
