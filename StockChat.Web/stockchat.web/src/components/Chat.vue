<template>
  <div class="container">
    <div class="row">
      <h1>Stock Chat</h1>
      <ul ref="chatList" class="list-group list-group-flush col-12 chat-list">
        <li class="list-group-item text-left" :key="message.date" v-for="message in messageList">
          {{ formatTime(message.date) }}
          <strong>{{ message.username }}:</strong>
          {{ message.text }}
        </li>
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
    if (localStorage.username == null || localStorage.token == null) {
      this.$router.push("Login");
    }

    stockApi.addToken(localStorage.token);

    this.$messagesHub.$on("message-received", this.receiveMessage);
    this.$messagesHub.$on("stock-received", this.receiveStock);
    this.loadMessageHistory();
  },
  mounted() {
    let list = this.$refs.chatList;
    let observer = new MutationObserver(this.scrollBottom);
    observer.observe(list, { childList: true });
  },
  methods: {
    loadMessageHistory() {
      let messages = stockApi
        .getMessagesHistory()
        .then(response => {
          this.messageList.push(...response);
        })
        .catch(err => console.log(err));
    },
    async sendMessage() {
      if (this.newMessage === null || this.newMessage === "") {
        return;
      }
      let stockRequest = this.newMessage.match("/stock=.*");
      if (stockRequest !== null) {
        let code = stockRequest[0].split("=")[1];
        if (code != null) {
          const self = this;
          stockApi
            .getStock(code)
            .then(stock => {
              this.cleanUp();
            })
            .catch(() => {
              self.addMessage(
                "There was an error trying to get stocks for " + code
              );
            });
        }
      } else {
        let posted = await stockApi
          .postMessage(this.newMessage)
          .then(response => {
            this.cleanUp();
          })
          .catch(err => console.log(err));
      }
    },
    addMessage(value) {
      let message = {
        date: Date.now(),
        text: value
      };
      this.messageList.push(message);
      this.cleanUp();
    },
    receiveMessage(message) {
      this.messageList.push(message);
    },
    receiveStock(stock) {
      let self = this;
      let message = {
        date: Date.now(),
        text: this.formatStock(stock),
        username: "StockBot"
      };
      self.messageList.push(message);
    },
    formatTime(date) {
      let dateObj = new Date(date);
      let formatedTime =
        (dateObj.getHours() % 12 || 12) +
        ":" +
        (dateObj.getMinutes() >= 10
          ? dateObj.getMinutes()
          : "0" + dateObj.getMinutes()) +
        ":" +
        (dateObj.getSeconds() >= 10
          ? dateObj.getSeconds()
          : "0" + dateObj.getSeconds());

      return formatedTime;
    },
    formatStock(stock) {
      return stock.symbol + " stock is $" + stock.price + " per share.";
    },
    scrollBottom() {
      let list = this.$refs.chatList;
      list.scrollTop = list.scrollHeight;
    },
    cleanUp() {
      this.newMessage = "";
    }
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