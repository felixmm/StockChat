<template>
  <div class="container">
    <div class="row">
      <h1>Stock Chat</h1>
      <ul ref="chatList" class="list-group list-group-flush col-12 chat-list">
        <li
          class="list-group-item text-left"
          :key="message.date"
          v-for="message in messageList"
        >{{ new Date(message.date).getHours()}}:{{ new Date(message.date).getMinutes()}}:{{ new Date(message.date).getSeconds()}}: {{ message.text }}</li>
      </ul>
    </div>
    <div class="row mt-3 mb-5">
      <input
        class="col-10"
        placeholder="chat here"
        v-model="newMessage"
        v-on:keyup.enter="sendMessage"
      />
      <button class="col-2" v-on:click="sendMessage">Send</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "Chat",
  data: () => {
    return {
      messageList: [],
      newMessage: ""
    };
  },
  methods: {
    sendMessage() {
      var message = {
        date: Date.now(),
        text: this.newMessage
      };
      this.messageList.push(message);
      this.newMessage = "";
      this.scrollBottom();
    },
    scrollBottom() {
      var list = this.$refs.chatList;
      list.scrollTop = list.scrollHeight;
    }
  },
  mounted() {
    var list = this.$refs.chatList;
    var observer = new MutationObserver(this.scrollBottom);
    observer.observe(list, { childList: true });
  }
};
</script>

<style scoped>
.chat-list {
  height: 600px;
  overflow: auto;
  border: 1px solid black;
}
</style>