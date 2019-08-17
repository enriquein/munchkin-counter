<template lang="pug">
    #app
        PlayerCount(v-if="!gameStarted")
        div(v-else)
            | Game started!
</template>

<script lang="ts">
import { mapState } from 'vuex';
import PlayerCount from './PlayerCount.vue';

export default {
    components: { PlayerCount },
    computed: mapState(['gameStarted'])
};
</script>