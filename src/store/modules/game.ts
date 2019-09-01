import Player from "../../player";

function getInitialState () {
    return {
        gameStarted: false,
        gameOver: false
    };
};

const gameModule = {
    namespaced: true,
    state: getInitialState(),
    mutations: {
        startGame (state) {
            state.gameStarted = true;
        },
        resetState (state) {
            Object.assign(state, getInitialState());
        }
    },
    actions: {
        startGame ({dispatch}) {
            dispatch('startGame');
        },
        resetState ({dispatch}) {
            dispatch('resetState');
        }
    }
}

export default gameModule;