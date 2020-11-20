module Player.View

open Player.Types
open Fable.React.Props
open Fable.React.Helpers
open Fable.React.Standard
open Fable.React

let private makeButton (text: string) (fn: unit -> unit) =
    button
        [   ClassName "btn-lg btn-primary"
            OnClick (fun _ -> fn())
            Type "button"
        ]
        [ str text ]


let private totalText text =
    span
        [ ClassName "level-text" ]
        [ str text ]

let private statButtons (text: string) (increaseFn: unit -> unit) (decreaseFn: unit -> unit) (valueFn: unit -> int) (hasTopMargin: bool) =
    let levelText =
        span
            [ ClassName "label-text right-margin" ]
            [ str text ]

    let buttons =
        span
            [   ClassName "right-margin" ]
            [   makeButton "+" increaseFn
                span [] [ str " "]
                makeButton "-" decreaseFn
            ]

    let totalText =
        span
            [ ClassName "level-text" ]
            [ str <| valueFn().ToString() ]
    div
        [ classList [ ("top-margin", hasTopMargin) ] ]
        [   levelText
            buttons
            totalText
        ]

let view (model: Model) dispatch : ReactElement =
    let cardHeaderContent =
        if model.IsWinner then
            span
                [ ClassName "winner-text"]
                [ str <| sprintf "🏆 👑 %s wins! 👑 🏆" model.Name]
         else
            input
                [   ClassName "form-control name-field"
                    Type "text"
                    Value model.Name
                    OnFocus (fun e ->
                        let target = e.target :?> Browser.Types.HTMLInputElement
                        target.select()
                    )
                    OnChange (fun e -> dispatch (ChangeName e.Value))
                ]

    let cardHeader =
        div
            [   ClassName "card-header" ]
            [   h3
                    []
                    [ cardHeaderContent ]
            ]

    let levelButtons =
        statButtons
            "Level"
            (fun () -> dispatch IncreaseLevel)
            (fun () -> dispatch DecreaseLevel)
            (fun () -> model.Level)
            false

    let bonusesButtons =
        statButtons
            "Bonuses"
            (fun () -> dispatch IncreaseBonuses)
            (fun () -> dispatch DecreaseBonuses)
            (fun () -> model.Bonuses)
            true

    let cursesButtons =
        statButtons
            "Curses"
            (fun () -> dispatch IncreaseCurses)
            (fun () -> dispatch DecreaseCurses)
            (fun () -> model.Curses)
            true

    let totalSection =
        div
            [   ClassName "top-margin"]
            [   span
                    [   ClassName "label-text right-margin" ]
                    [   str "Total" ]
                span
                    [   ClassName "total-text" ]
                    [   str <| model.Total.ToString() ]
            ]

    let cardBody =
        div
            [   ClassName "card-body" ]
            [   levelButtons
                bonusesButtons
                cursesButtons
                totalSection
            ]

    div
        [   ClassName "col-lg-3 col-md-4 col-sm-6 col-xs-12" ]
        [   div
                [   classList [ ("opaque", model.IsOpaque); ("card", true) ] ]
                [   cardHeader
                    cardBody
                ]
        ]