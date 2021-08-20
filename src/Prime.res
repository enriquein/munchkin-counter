module InputText = {
    @react.component @module("primereact/inputtext")
    external make: (~className: string=?, ~value: string, ~onChange: ('evt => unit)=?, ~onFocus: ('evt => unit)=?, ~ref: 'r=?) => React.element = "InputText"
}

module Button = {
    @react.component @module("primereact/button")
    external make: (~label: string, ~onClick: 'evt => unit) => React.element = "Button"
}

module Panel = {
    @react.component @module("primereact/panel")
    external make: (~header: React.element=?, ~className: string=?, ~children: React.element) => React.element = "Button"
}

module Utils = {
    type t = {
        @set "ripple": bool
    };

    @module external primeReact: t = "primereact/utils"
    let init = (ripple: bool) => {
        primeReact["ripple"] = ripple
    }
}
