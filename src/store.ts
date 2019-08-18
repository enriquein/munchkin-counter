import Vue from 'vue';
import Vuex from 'vuex';
import { findIndex } from 'lodash';
import Player from './player';

Vue.use(Vuex);

let store = new Vuex.Store({
    state: {
        playerNum: 0,
        gameStarted: false,
        gameOver: false,
        players: []
    },
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
            state.gameStarted = false;
            state.playerNum = 0;
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