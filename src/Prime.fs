module Prime
open Fable.React
open Fable.React.Props

let primeClasses (classNames: string) : HTMLAttr =
    HTMLAttr.Custom ("className", classNames)

let primeButton (props: obj) : ReactElement =
    ofImport "Button" "primereact/button" props []

let primePanel (props: obj) (elems: ReactElement list) : ReactElement =
    ofImport "Panel" "primereact/panel" props elems

let primeCard (props: obj) (elems: ReactElement list) : ReactElement =
    ofImport "Card" "primereact/card" props elems

let primeTextBox (props: obj) : ReactElement =
    ofImport "InputText" "primereact/inputtext" props []
