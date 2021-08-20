// Imports for bundler
%%raw("require('primereact/resources/themes/saga-blue/theme.css')")
%%raw("require('primereact/resources/primereact.min.css')")
%%raw("require('primeicons/primeicons.css')")
%%raw("require('primeflex/primeflex.css')")

Prime.Utils.init(true)

switch(ReactDOM.querySelector("#app")) {
| Some(root) => ReactDOM.render(<Game />, root)
| None => ()
}