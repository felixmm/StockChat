<template>
  <div class="container">
    <div class="row">
      <h1>Stock Chat</h1>
      <ul ref="chatList" class="list-group list-group-flush col-12 chat-list">
        <li
          class="list-group-item text-left"
          :key="message.date"
          v-for="message in messageList"
        >{{ formatTime(message.date) }}: {{ message.text }}</li>
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
import stockApi from "@/services/stockApi";
import connectionHub from "@/services/messagesHub";

export default {
  name: "Chat",
  data: () => {
    return {
      messageList: [],
      newMessage: ""
    };
  },
  created() {
    this.$messagesHub.$on("message-received", this.receiveMessage);
    this.$messagesHub.$on("stock-received", this.receiveStock);
  },
  methods: {
    async sendMessage() {
      let stockRequest = this.newMessage.match("/stock=.*");
      if (stockRequest !== null) {
        let code = stockRequest[0].split("=")[1];
        if (code != null) {
          const self = this;
          stockApi
            .getStock(code)
            .then(stock => {
              self.addMessage(selft.formatStock(stock));
            })
            .catch(() => {
              self.addMessage(
                "There was an error trying to get stocks for " + code
              );
            });
        }
      } else {
        let message = this.newMessage;
        this.addMessage(message);
        let posted = await stockApi.postMessage(message).then(response => {
          console.log(response);
        });
      }
    },
    addMessage(value) {
      let message = {
        date: Date.now(),
        text: value
      };
      this.messageList.push(message);
      this.newMessage = "";
      this.scrollBottom();
    },
    receiveMessage(message) {
      this.messageList.push(message);
    },
    receiveStock(stock) {
      let self = this;
      self.messageList.push(self.formatStock(stock));
    },
    formatTime(date) {
      let dateObj = new Date(date);
      let formatedTime =
        (dateObj.getHours() % 12 || 12) +
        ":" +
        dateObj.getMinutes() +
        ":" +
        dateObj.getSeconds();

      return formatedTime;
    },
    formatStock(stock) {
      return stock.symbol + " stock is $" + stock.price + " per share.";
    },
    scrollBottom() {
      let list = this.$refs.chatList;
      list.scrollTop = list.scrollHeight;
    }
  },
  mounted() {
    let list = this.$refs.chatList;
    let observer = new MutationObserver(this.scrollBottom);
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