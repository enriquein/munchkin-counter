import Vue from 'vue';
import App from './components/App.vue';
import store from './store';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import './style.css';

Vue.use(BootstrapVue);

var vm = new Vue({
    el: '#app',
    store,
    render: h => h(App)
});

export default vm;