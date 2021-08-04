////"use strict";
//$(function () {

//    $("#sendToRabbit").click(function () {
//        debugger;
//        //form encoded data
//        var dataType = 'application/x-www-form-urlencoded; charset=utf-8';
//        var data = $('form').serialize();

//        //JSON data
//        var dataType = 'application/json; charset=utf-8';
//        var data = {
//            FirstName: 'Andrew',
//            LastName: 'Lock',
//            Age: 31
//        }

//        console.log('Submitting form...');
//        $.ajax({
//            type: 'POST',
//            url: '/Person/Index',
//            dataType: 'json',
//            contentType: dataType,
//            data: data,
//            success: function (result) {
//                console.log('Data received: ');
//                console.log(result);
//            }
//        });
//    });

//    //document.getElementById("sendButton").addEventListener("click", function (event) {
//    //    var user = document.getElementById("userInput").value;
//    //    var message = document.getElementById("messageInput").value;
//    //    connection.invoke("SendMessage", user, message).catch(function (err) {
//    //        return console.error(err.toString());
//    //    });
//    //    event.preventDefault();
//    //});

//    //document.getElementById("sendButton2").addEventListener("click", function (event) {
//    //    connection.invoke("SendObject", "").catch(function (err) {
//    //        return console.error(err.toString());
//    //    });
//    //    event.preventDefault();
//    //});

//    //document.getElementById("joinToGroupButton").addEventListener("click", function (event) {
//    //    var cashId = document.getElementById("cashId").value;
//    //    connection.invoke("AddToGroup", cashId).catch(function (err) {
//    //        return console.error(err.toString());
//    //    });
//    //    event.preventDefault();
//    //});

//    //document.getElementById("sendToRabbit").addEventListener("click", function (event) {
//    //    debugger;
//    //    var xhr = new XMLHttpRequest();
//    //    xhr.open("POST", "Create", true);
//    //    xhr.setRequestHeader('Content-Type', 'application/json');
//    //    xhr.send(JSON.stringify({
//    //        id: document.getElementById('Id').value,
//    //        name: document.getElementById('Name').value,
//    //        code: document.getElementById('Code').value,
//    //        barcode: document.getElementById('Barcode').value,
//    //        comment: document.getElementById('Comment').value,
//    //        clientId: document.getElementById('ClientId').value,
//    //        isActive: document.getElementById('IsActive').value,
//    //    }));
//    //});
//});