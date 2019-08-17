import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

let store = new Vuex.Store({
    state: {
        playerNum: 0,
        gameStarted: false
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
        }
    }
});

export default store;