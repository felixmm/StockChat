# Stock Chat

Project that allows users to Login or Register, and join a group chat with all other users.

The chat also has a command to search for current pricess on stocks with

```
/stock=[stock_code]
```

Ex: /stock=appl.us withh retreive the current price for the APPLE stocks and display them in the chat.

## Project Structure

The solution contains three projects:

- **StockChat.API**
  REST API that handles the user registration and login and chat messages. Contains events to alert when a new message or stock request is received.

- **StockChat.Bot**
  Bot that handles the stock requests specifically, using a third party API to get the current information for any given stock

- **StockChat.Web**
  Barebones Vue web app that utilizes the REST API and displays the messages and stocks in a chat box

## How to run

### Requirements

The project uses dotnet core v3.1 and node v10.15.13

- [DotNet Core](https://dotnet.microsoft.com/download)
- [NodeJS](https://nodejs.org/en/)

### Restore, compile and run

Clone the repository, go into the Stock.API project and [restore the nuget packages](https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore).

In the Nuget Package Management console, run the migrations and create the local SqLite database

```
dotnet ef database update
```

Then in Visual Studio set the StockChat.API project as Startup and compile/run. The API should be running on **http://localhost:1554**

Then, go into the Stock.Web project, restore the node packages and run the Vue app

```
cd StockChat.Web
npm install
npm run serve
```

The web app should be running in **http://localhost:8080** and you should be able to Register and or Login and move to the chat box.
