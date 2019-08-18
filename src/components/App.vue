<template lang="pug">
    #app
        PlayerCount(v-if="!gameStarted")
        .container-fluid(v-else)
            .row
                .col-md-12 &nbsp;
            .row
                Player(v-for="(player, index) in players" :key="player.name" :player-index="index")

</template>

<script lang="ts">
import { mapState } from 'vuex';
import PlayerCount from './PlayerCount.vue';
import Player from './Player.vue';

export default {
    components: { PlayerCount, Player },
    computed: mapState(['gameStarted', 'players'])
};
</script>