module App

open Fable.Core
open Fable.Import
open Fable.React
open Elmish
open Elmish.Debug
open Elmish.React
open Player


let init () =
    Player.StateManagement.init "test 1"

Program.mkProgram init Player.StateManagement.update Player.View.view
|> Program.withReactHydrate "app"
|> Program.withDebugger
|> Program.run