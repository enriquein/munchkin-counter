<template lang="pug">
    -
        let capitalize = str => {
            let val = str ? str[0].toUpperCase() + str.slice(-(str.length -1)) : ''
            return val + '&nbsp;'.repeat(7 - val.length)
        }

    mixin stat(type, hasTopMargin)
        div(class=`${hasTopMargin ? 'top-margin' : ''}`)
            span.label-text.right-margin(v-html=`'${capitalize(type)}'`)
            span.right-margin
                button.btn-lg.btn-primary(type="button" @click=`addOne('${type}')`) +
                span &nbsp;
                button.btn-lg.btn-primary(type="button" @click=`removeOne('${type}')`) -
            span.level-text {{ player.#{type} }}


    .col-lg-3.col-md-4.col-sm-6.col-xs-12
        .panel.panel-default(:class="{ opaque: gameOver && !playerIsWinner}")
            .panel-heading
                h3.panel-title(v-if="!playerIsWinner")
                    input.form-control.name-field(type="text" @click="selectAllText" v-model="name")
                h3.panel-title(v-else)
                    span.winner-text 🏆 👑 {{ name }} wins! 👑 🏆
            .panel-body
                +stat("level", false)
                +stat("bonuses", true)
                +stat("curses", true)
                .top-margin
                    span.label-text.right-margin Total
                    span.total-text {{ player.total() }}
</template>

<script lang="ts">
import { mapMutations, mapState } from 'vuex';
import { selectAllText } from '../helpers/dom-helpers';

export default {
    props: { playerIndex: Number },
    computed: {
        player () {
            return this.$store.state.players[this.playerIndex];
        },
        playerIsWinner () {
            return this.player.level >= 10;
        },
        name: {
            get() {
                return this.player.name;
            },
            set(value: string) {
                this.$store.commit('setPlayerName', {
                    index: this.playerIndex,
                    name: value
                });
            }
        },
        ...mapState(['gameOver'])
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