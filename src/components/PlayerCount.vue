<template lang="pug">
    .container-fluid
        .row &nbsp;
        .row
            .col-md-4.col-sm-6
                .form-group
                    label(for="playerCount")
                        | How many players are going to play?
                    input.form-control(type="text" v-model="playerNum" @click="selectAllText")

                button.btn.btn-primary(type="button" @click="startGame") Start Game
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