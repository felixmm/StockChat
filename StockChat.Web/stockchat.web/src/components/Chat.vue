<template>
<div>
    <div>
        <ul>
            <li :key="message.date" v-for="message in messageList">
                {{ message.text }}
            </li>
        </ul>
    </div>
    <div>
        <input v-model="newMessage" v-on:keyup.enter="sendMessage" />
        <button v-on:click="sendMessage">Send</button>
    </div>
</div>
</template>

<script>
export default {
    name: 'Chat',
    data: () => {
        return {
            messageList: [],
            newMessage: "",
        }
    },
    methods: {
        sendMessage() {
            var message = {
                date: Date.now(),
                text: this.newMessage
            };
            this.messageList.push(message);
        }
    }
}
</script>

<style scoped>
</style>