<template lang="pug">
    #app
        PlayerCount(v-if="!gameStarted")
        b-container(fluid v-else)
            b-row.mt-4.mb-4
                Player(v-for="(player, index) in players" :player-index="index")
            b-row
                b-col
                    b-button(type="button" variant="danger" @click="confirmReset") Start new game
</template>

<script lang="ts">
import { mapState } from 'vuex';
import PlayerCount from './PlayerCount.vue';
import Player from './Player.vue';

export default {
    components: { PlayerCount, Player },
    computed: mapState(['gameStarted', 'players']),
    methods: {
        confirmReset () {
            let answer = prompt('Do you want to start a new game? (Yes or No)');
            if (answer.toLowerCase() === 'yes' || answer.toLowerCase() === 'y') {
                this.$store.commit('resetGame');
            }
        }
    }
};
</script>