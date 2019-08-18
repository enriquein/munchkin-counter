<template lang="pug">
    - let capitalize = str => str ? str[0].toUpperCase() + str.slice(-(str.length -1)) : ''

    mixin stat(type, hasTopMargin)
        div(class=`${hasTopMargin ? 'top-margin' : ''}`)
            span.label-text.right-margin #{capitalize(type)}
            span.right-margin
                button.btn-lg.btn-primary(type="button" @click=`addOne('${type}')`) +
                button.btn-lg.btn-primary(type="button" @click=`removeOne('${type}')`) -
            span.level-text {{ bonuses }}


    .col-lg-3.col-md-4.col-sm-6.col-xs-12
        .panel.panel-default(:class="{ opaque: !playerIsWinner()}")
            .panel-heading
                h3.panel-title(v-if="level < 10")
                    input.form-control.name-field(type="text" @click="selectAllText" v-model="name")
                h3.panel-title(v-else)
                    span.winner-text 🏆 👑 {{ name }} wins! 👑 🏆
                .panel-body
                    +stat("level", false)
                    +stat("bonuses", true)
                    +stat("curses", true)
                    .top-margin
                        span.label-text.right-margin Total
                        span.total-text {{ total }}
</template>

<script lang="ts">
import { mapMutations } from 'vuex';
import { selectAllText } from '../helpers/dom-helpers';

export default {
    props: { playerIndex: Number },
    computed: {
        name: {
            get() {
                return this.$store.state.players[this.playerIndex].name;
            },
            set(value: string) {
                this.$store.commit('setPlayerName', {
                    index: this.playerIndex,
                    name: value
                });
            }
        },
        level () {
            return this.$store.getters.getPlayerStat(this.playerIndex, 'level');
        },
        bonuses () {
            return this.$store.getters.getPlayerStat(this.playerIndex, 'bonuses');
        },
        curses () {
            return this.$store.getters.getPlayerStat(this.playerIndex, 'curses');
        },
        total () {
            return this.$store.getters.getPlayerStat(this.playerIndex, 'total');
        },
        playerIsWinner () {
            return this.$store.getters.winningPlayerIndex === this.playerIndex;
        }
    },
    methods: {
        selectAllText,
        addOne (type) {
            this.$store.commit('incrementStat', {
                index: this.playerIndex,
                stat: type
            })
        },
        removeOne (type) {
            this.$store.commit('decrementStat', {
                index: this.playerIndex,
                stat: type
            })
        }
    }
}
</script>

<style lang="stylus" scoped>
.name-field
    display: inline-block !important
</style>