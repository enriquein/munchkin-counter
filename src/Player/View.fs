namespace PlayerImplementation

[<AutoOpen>]
module View =
    open System
    open PlayerImplementation.Types
    open Fable.Core
    open Fable.Core.JsInterop
    open Browser.Types
    open Fable.React.Props
    open Fable.React.Helpers
    open Fable.React.Standard
    open Fable.React

    let primeClasses (classNames: string) : HTMLAttr =
        HTMLAttr.Custom ("className", classNames)

    let private primeButton (props: obj) : ReactElement =
        ofImport "Button" "primereact/button" props []

    let private primePanel (props: obj) (elems: ReactElement list) : ReactElement =
        ofImport "Panel" "primereact/panel" props elems

    let private primeCard (props: obj) (elems: ReactElement list) : ReactElement =
        ofImport "Card" "primereact/card" props elems

    let private primeTextBox (props: obj) : ReactElement =
        ofImport "InputText" "primereact/inputtext" props []

    let private makeButton (text: string) (fn: unit -> unit) (disabled: bool) =
        primeButton
            {|
                className = "p-button-lg"
                onClick = (fun _ -> fn())
                icon = if text = "+" then "pi pi-plus" else "pi pi-minus"
                disabled = disabled
            |}


    let private totalText text =
        span
            [ ClassName "level-text" ]
            [ str text ]

    let private statButtons (text: string) (increaseFn: unit -> unit) (decreaseFn: unit -> unit) (valueFn: unit -> int) (hasTopMargin: bool) (disabled: bool) =
        let levelText =
            div
                [   primeClasses "p-col-fixed label-text"
                    Style [Width "100px"]
                ]
                [   str text]

        let buttons =
            div
                [   primeClasses "p-col-fixed"
                    Style [Width "125px"]
                ]
                [   makeButton "+" increaseFn disabled
                    span [] [ str " "]
                    makeButton "-" decreaseFn disabled
                ]

        let totalText =
            div
                [   primeClasses "p-col-fixed level-text p-text-center"
                    Style [Width "30px"]
                ]
                [   str <| valueFn().ToString() ]
        div
            [   primeClasses "p-grid flex-align-center" ]
            [   levelText
                buttons
                totalText
            ]

    let view (model: Model) dispatch : ReactElement =
        let cardHeader =
            if model.IsWinner then
                span
                    [ ClassName "winner-text"]
                    [ str <| sprintf "🏆 %s wins! 👑" model.Name]
             else
                primeTextBox
                    {|  className = "name-field"
                        value = model.Name
                        onFocus =
                            (fun (e: FocusEvent) ->
                                let target = e.target :?> Browser.Types.HTMLInputElement
                                target.select()
                            )
                        onChange = (fun (e: Event) -> dispatch (ChangeName e.Value))
                        disabled = model.IsDisabled
                    |}

        let levelButtons =
            statButtons
                "Level"
                (fun () -> dispatch IncreaseLevel)
                (fun () -> dispatch DecreaseLevel)
                (fun () -> model.Level)
                false
                model.IsDisabled

        let bonusesButtons =
            statButtons
                "Bonuses"
                (fun () -> dispatch IncreaseBonuses)
                (fun () -> dispatch DecreaseBonuses)
                (fun () -> model.Bonuses)
                true
                model.IsDisabled

        let cursesButtons =
            statButtons
                "Curses"
                (fun () -> dispatch IncreaseCurses)
                (fun () -> dispatch DecreaseCurses)
                (fun () -> model.Curses)
                true
                model.IsDisabled

        let totalSection =
            div
                [   primeClasses "p-grid flex-align-center"]
                [   div
                        [   primeClasses "p-col-fixed label-text"
                            Style [Width "100px"]
                        ]
                        [   str "Total"]

                    div
                        [   primeClasses "p-col-fixed label-text"
                            Style [Width "125px"]
                        ]
                        [   str ""]

                    div
                        [   primeClasses "p-col-fixed total-text p-text-center"
                            Style [Width "30px"]
                        ]
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
            [   primeClasses "player-panel p-col-fixed"
                Style [Width "320px"]
            ]
            [   primePanel
                    {|  header = cardHeader
                        className = if model.IsDisabled then "opaque" else ""
                    |}
                    [
                        cardBody
                    ]
            ]