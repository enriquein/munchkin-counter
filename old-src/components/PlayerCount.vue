<template lang="pug">
    b-container(fluid)
        b-row.mt-4
            b-col(md="4" sm="6")
                .form-group
                    label(for="playerCount")
                        | How many players are going to play?
                    input.form-control(type="text" v-model="playerNum" @click="selectAllText")

                b-button(type="button" variant="primary" @click="startGame") Start Game
</template>

<script lang="ts">
import Vue from 'vue';
import { mapMutations } from 'vuex';
import { selectAllText } from '../helpers/dom-helpers';

export default {
    computed: {
        playerNum: {
            get() {
                return this.$store.state.playerNum;
            },
            set(value: string) {
                let playerNum = value.replace(/\D+/i, '');

                // Super duper hack to force vue to refresh the value
                if (playerNum.length !== value.length) {
                    this.$store.commit('setPlayerNum', { playerNum: 0 });
                }

                this.$store.commit('setPlayerNum', { playerNum });
            }
        }
    },
    methods: {
        selectAllText,
        ...mapMutations([
            'startGame'
        ])
    }
}
</script>