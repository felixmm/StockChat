const BASEURL = "http://localhost:1554/";

export default {
  getStock: (code) => {
    let url = BASEURL + "stocks/" + code;
    let stockPromise = fetch(url, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    })
      .then((response) => response.json())
      .then((data) => {
        return new Promise(function(resolve, reject) {
          if (data === null) {
            reject(data);
          }

          resolve(data);
        });
      })
      .catch(() => {
        return null;
      });

    return stockPromise;
  },
  postMessage: async (message) => {
    let messagePromise = await fetch(BASEURL + "messages", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        text: message,
      }),
    })
      .then((response) => response.json())
      .then((data) => {
        return new Promise(function(resolve, reject) {
          if (data === null) {
            reject(data);
          }
          resolve(data);
        });
      })
      .catch(() => {
        return null;
      });

    return messagePromise;
  },
  getMessagesHistory: () => {
    let messagesPromise = fetch(BASEURL + "messages", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    }).then((response) =>
      response.json().then((data) => {
        return new Promise(function(resolve, reject) {
          if (data === null) {
            reject(data);
          }
          resolve(data);
        });
      })
    );

    return messagesPromise;
  },
};
