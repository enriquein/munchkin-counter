import Vue from 'vue';
import store from './store';
import App from './components/App.vue';

var vm = new Vue({
    el: '#app',
    store,
    render: h => h(App)
});

export default vm;