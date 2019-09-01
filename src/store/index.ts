import Vue from 'vue';
import Vuex from 'vuex';
import gameModule from './modules/game';
import playerModule from './modules/player';

Vue.use(Vuex);

let store = new Vuex.Store({
    modules: {
        gameModule,
        playerModule
    },
    actions: {
        resetGame (context) {
            context.dispatch('gameModule/resetGame');
            context.dispatch('playerModule/resetGame');
        }
    }
});

export default store;