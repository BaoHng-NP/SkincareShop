// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();


connection.on("LoadAllOrder", function () {
    location.href = '/Customer/OrderHistory';
});

connection.on("LoadAllStore", function () {
    window.location.href = '/store?pageNumber=2';
});


connection.start().then(function () {
    console.log("Connected to the SignalRHub");


}).catch(function (err) {
    console.error(err.toString());
});