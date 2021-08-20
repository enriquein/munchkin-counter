@react.component
let make = () => {
    let (playerCount, setPlayerCount) = React.useState(_ => 0)
    let (gameStarted, setGameStarted) = React.useState(_ => false)

    if gameStarted == false {
        <PlayerSetup playerCount setPlayerCount setGameStarted />
    } else {
        <Player initialName="lolo" isWinner=false setIsWinner={setGameStarted} isOpaque=false />
    }
}