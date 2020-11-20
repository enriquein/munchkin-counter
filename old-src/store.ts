import Vue from 'vue';
import Vuex from 'vuex';
import { findIndex } from 'lodash';
import Player from './player';

Vue.use(Vuex);

let getInitialState = () => {
    return {
        playerNum: 0,
        gameStarted: false,
        gameOver: false,
        players: []
    };
};

let store = new Vuex.Store({
    state: getInitialState(),
    mutations: {
        setPlayerNum (state, payload) {
            state.playerNum = payload.playerNum;
        },
        startGame (state) {
            if (state.playerNum > 0) {
                state.gameStarted = true;

                for (let i = 1; i <= state.playerNum; i++) {
                    state.players.push(new Player(i));
                }
            }
        },
        resetGame (state) {
            Object.assign(state, getInitialState());
        },
        setPlayerName (state, payload) {
            state.players[payload.index].name = payload.name;
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
    }
});

export default store;