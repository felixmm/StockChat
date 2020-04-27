import Vue from "vue";
import App from "./App.vue";
import VueRouter from "vue-router";
import QuestionHub from "./services/messagesHub";
import "bootstrap/dist/css/bootstrap.min.css";

import Chat from "./components/Chat.vue";
import Login from "./components/Login.vue";

Vue.use(VueRouter);
Vue.use(QuestionHub);
Vue.config.productionTip = false;

const routes = [
  { path: "/chat", component: Chat },
  { path: "/login", component: Login },
];

const router = new VueRouter({
  routes,
});

new Vue({
  router,
  render: (h) => h(App),
}).$mount("#app");
