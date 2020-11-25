namespace PlayerImplementation

[<AutoOpen>]
module View =
    open System
    open PlayerImplementation.Types
    open Fable.React.Props
    open Fable.React.Helpers
    open Fable.React.Standard
    open Fable.React

    let private makeButton (text: string) (fn: unit -> unit) (disabled: bool) =
        button
            [   ClassName "btn-lg btn-primary"
                OnClick (fun _ -> fn())
                Type "button"
                Disabled disabled
            ]
            [ str text ]


    let private totalText text =
        span
            [ ClassName "level-text" ]
            [ str text ]

    let private statButtons (text: string) (increaseFn: unit -> unit) (decreaseFn: unit -> unit) (valueFn: unit -> int) (hasTopMargin: bool) (disabled: bool) =
        let levelText =
            span
                [ ClassName "label-text right-margin"
                  DangerouslySetInnerHTML { __html = text } ]
                []

        let buttons =
            span
                [   ClassName "right-margin" ]
                [   makeButton "+" increaseFn disabled
                    span [] [ str " "]
                    makeButton "-" decreaseFn disabled
                ]

        let totalText =
            span
                [ ClassName "level-text" ]
                [ str <| valueFn().ToString() ]
        div
            [   classList [ ("top-margin", hasTopMargin) ] ]
            [   levelText
                buttons
                totalText
            ]

    let padded (text: string) =
        let padding = [ for i in 1 .. (7 - text.Length) -> "&nbsp;" ]
        sprintf "%s%s" text (String.Concat(padding))

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
                        Disabled model.IsDisabled
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
                (padded "Level")
                (fun () -> dispatch IncreaseLevel)
                (fun () -> dispatch DecreaseLevel)
                (fun () -> model.Level)
                false
                model.IsDisabled

        let bonusesButtons =
            statButtons
                (padded "Bonuses")
                (fun () -> dispatch IncreaseBonuses)
                (fun () -> dispatch DecreaseBonuses)
                (fun () -> model.Bonuses)
                true
                model.IsDisabled

        let cursesButtons =
            statButtons
                (padded "Curses")
                (fun () -> dispatch IncreaseCurses)
                (fun () -> dispatch DecreaseCurses)
                (fun () -> model.Curses)
                true
                model.IsDisabled

        let totalSection =
            div
                [   ClassName "top-margin"]
                [   span
                        [   ClassName "label-text right-margin"
                            DangerouslySetInnerHTML { __html = padded "Total" }
                        ]
                        []
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
                    [   classList [ ("opaque", model.IsDisabled); ("card", true) ] ]
                    [   cardHeader
                        cardBody
                    ]
            ]