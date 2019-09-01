import { findIndex } from 'lodash';

function getInitialState () {
    return {
        players: [],
        playerCount: 0
    };
};

const playerModule = {
    namespaced: true,
    state: getInitialState(),
    mutations: {
        startGame (state, payload) {
            if (payload > 0) {
                state.gameStarted = true;

                for (let i = 1; i <= state.playerNum; i++) {
                    state.players.push(new Player(i));
                }
            }
        },
        incrementStat (state, payload) {
            state.players[payload.index][payload.stat]++;

            if (payload.stat === 'level' && state.players[payload.index].level >= 10)
                state.gameOver = true;
        },
        decrementStat (state, payload) {
            if (state.players[payload.index][payload.stat] <= 0)
                return;

            state.players[payload.index][payload.stat]--;

            // Need to reset gameOver if we ended prematurely by mistake and this
            // decrement was intended to remove winner/gameOver status
            if (state.gameOver === true)
                state.gameOver = findIndex(state.players, player => player.level >= 10) >= 0;
        }
    },
    actions: {
        startGame ({dispatch}, playerCount) {

        }
    },
  getters: { ... }
}

export default playerModule;