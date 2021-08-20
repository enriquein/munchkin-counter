type t = {
    name: string,
    level: int,
    bonuses: int,
    curses: int,
    total: int
}

module CardHeader  = {
    @send external select: Dom.element => unit = "select"

    @react.component
    let make = (~playerName: string, ~setPlayerName, ~isWinner) => {
        let onPlayerNameChanged = evt => {
            ReactEvent.Form.preventDefault(evt)
            let value = ReactEvent.Form.target(evt)["value"]
            setPlayerName(_prev => value);
        }

        let selectAllOnFocus = evt => {
            ReactEvent.Form.target(evt)->select
        }

        {
            isWinner
            ? <span className="winner-text">{React.string(`🏆 ${playerName} wins! 👑`)}</span>
            : <Prime.InputText className="name-field" value={playerName} onChange={onPlayerNameChanged} onFocus={selectAllOnFocus} />
        }
    }
}

@react.component
let make = (~initialName: string, ~isWinner: bool, ~setIsWinner, ~isOpaque: bool) => {
    let (playerName, setPlayerName) = React.useState(_ => initialName)

    <div className="player-panel p-col-fixed" style={ReactDOM.Style.make(~width="320px", ())}>
    <CardHeader playerName setPlayerName isWinner />

        // <Prime.Panel>

        // </Prime.Panel>
    </div>
}