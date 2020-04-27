import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";

export default {
  install(Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:1554/messagesHub")
      .configureLogging(LogLevel.Information)
      .build();

    const messagesHub = new Vue();
    Vue.prototype.$messagesHub = messagesHub;

    connection.on("sendMessage", (message) => {
      messagesHub.$emit("message-received", message);
    });

    connection.on("sendStock", (quote) => {
      messagesHub.$emit("stock-received", quote);
    });

    connection.start();
  },
};
