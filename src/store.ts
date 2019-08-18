import Vue from 'vue';
import Vuex from 'vuex';
import { findIndex } from 'lodash';

Vue.use(Vuex);

let store = new Vuex.Store({
    state: {
        playerNum: 0,
        gameStarted: false,
        players: []
    },
    getters: {
        getPlayerStat: state => (index, stat) => state.players[index][stat],
        getWinningPlayerIndex: state => {
            if (state.players.length === 0)
                return -1;

            return findIndex(state.players, player => player.level >= 10);
        }
    },
    mutations: {
        setPlayerNum (state, payload) {
            state.playerNum = payload.playerNum;
        },
        startGame (state) {
            state.gameStarted = true;
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
        },
        decrementStat (state, payload) {
            if (state.players[payload.index][payload.stat] > 0)
                state.players[payload.index][payload.stat]--;
        }
    }
});

export default store;