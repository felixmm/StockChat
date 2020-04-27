import Vue from "vue";
import App from "./App.vue";
import "bootstrap/dist/css/bootstrap.min.css";
import QuestionHub from "./services/messagesHub";

Vue.config.productionTip = false;

Vue.use(QuestionHub);

new Vue({
  render: (h) => h(App),
}).$mount("#app");
