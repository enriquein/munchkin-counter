
switch(ReactDOM.querySelector("#app")) {
| Some(root) => ReactDOM.render(<PlayerSetup />, root)
| None => ()
}