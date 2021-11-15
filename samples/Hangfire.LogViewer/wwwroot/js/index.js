"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/log-sink").build();

connection.on("PushEventLog", function (logEvent) {
    var li = document.createElement("li");

    document.getElementById("messagesList").prepend(li)
    li.innerHTML = `<b>[${logEvent.timestamp}][${logEvent.level}]</b> ${logEvent.renderedMessage}`;
});

connection.start();