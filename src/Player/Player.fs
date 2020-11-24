module Player

open Fable.React
open Elmish
open PlayerImplementation.View
open PlayerImplementation.State

type Model = PlayerImplementation.Types.Model
type Msg = PlayerImplementation.Types.Msg

let init (name: string) : Model * Cmd<Msg> =
    init name

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    update msg model

let view (model: Model) dispatch : ReactElement =
    view model dispatch
