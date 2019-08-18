<template lang="pug">
    #app
        PlayerCount(v-if="!gameStarted")
        .container-fluid(v-else)
            .row
                .col-md-12 &nbsp;
            .row
                Player(v-for="(player, index) in players" :player-index="index")
            .row
                .col-md-12
                    button.btn.btn-danger(type="button" @click="confirmReset") Start new game
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