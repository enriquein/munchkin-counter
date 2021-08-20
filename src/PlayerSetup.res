@react.component
let make = (~playerCount: int, ~setPlayerCount, ~setGameStarted) => {
    let (showError, setShowError) = React.useState(_ => false)

    let onClick = _ => {
        if playerCount >=3 && playerCount <= 10 {
            setShowError(_ => false)
            setGameStarted(_ => true)
        } else {
            setShowError(_ => true)
        }
    }

    let onChange = evt => {
        ReactEvent.Form.preventDefault(evt)
        let value = ReactEvent.Form.target(evt)["value"]
        setPlayerCount(_prev => value);
    }

    <div className="grid" style={ReactDOM.Style.make(~marginTop="15px", ~marginLeft="15px", ())}>
        <div className="col-4 sm:col-6">
            <div className="field p-component">
                <label className="block">
                    {React.string("How many players are going to play?")}
                </label>
                <Prime.InputText value={Js.Int.toString(playerCount)} onChange className="block" />
                {
                    showError
                        ? <div className="error-text">
                            {React.string("Please enter a number between 3 and 10.")}
                          </div>
                        : React.null
                }
            </div>
            <Prime.Button onClick label="Start Game" />
        </div>
    </div>
}