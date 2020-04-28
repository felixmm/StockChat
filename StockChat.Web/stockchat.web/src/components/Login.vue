<template>
  <div class="container">
    <h1>Stock Chat</h1>
    <div class="row">
      <div class="col">
        <h3 class="text-left mb-5">Login/Register</h3>
        <div class="mb-2">
          <label class="row">Username</label>
          <input class="row" type="text" v-model="user.username" v-on:keyup.enter="login" />
        </div>
        <div class="mb-2">
          <label class="row">Password</label>
          <input class="row" type="password" v-model="user.password" v-on:keyup.enter="login" />
        </div>
        <div class="text-left">
          <button v-on:click="login" v-on:keyup.enter="login">Login</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import stockApi from "@/services/stockApi";

export default {
  name: "Login",
  data: () => {
    return {
      user: {
        username: "",
        password: ""
      }
    };
  },
  methods: {
    login() {
      console.log("login clicked");
      let self = this;
      stockApi.login(this.user).then(data => {
        localStorage.username = data.username;
        localStorage.token = data.token;

        self.$router.push("Chat");
      });
    }
  }
};
</script>

<style scroped>
</style>