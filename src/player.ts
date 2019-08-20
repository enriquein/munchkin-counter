export default class Player {
    name: string;
    level: number;
    bonuses: number;
    curses: number;

    constructor(playerNumber) {
        this.name = "Player " + playerNumber;
        this.level = 0;
        this.bonuses = 0;
        this.curses = 0;
    }
}