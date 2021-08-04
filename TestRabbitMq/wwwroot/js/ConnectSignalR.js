"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/broadcasthub?cashId=124").build();
connection.start();

connection.on("ReceiveMessageHandler", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    alert(msg);
});


connection.on("ReceiveObjectHandler", function (message) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    alert(message.length + " object(s) recieved ...");
});