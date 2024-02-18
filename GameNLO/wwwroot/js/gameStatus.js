"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/StatusHub").build();

connection.on("UpdateStatus", function (status, latitude, longitude) {
    Swal.fire({
        position: "top-end",
        icon: "success",
        title: status,
        showConfirmButton: true
    });

    openBox(latitude, longitude);
});

connection.on("SendMessage", function (message, icon) {
    Swal.fire({
        position: "top-end",
        icon: icon,
        title: message,
        showConfirmButton: true
    });
});

connection.on("ValidateUser", function (isUserEligible, message) {
    validateUser(isUserEligible);

    if (!isUserEligible) {
        Swal.fire({
            position: "top-end",
            icon: "warning",
            title: message,
            showConfirmButton: true
        });
    }
});

connection.start().then(function () {
    validateUser(false); // Just to be protective
}).catch(function (err) {
    return console.error(err.toString());
});

function requestBox(latitude, longitude) {

    try {
        connection.invoke("SendCommand", latitude, longitude)
            .then(function () {
                openBox(latitude, longitude)
            })
    } catch (error) {
        return console.error(error.toString());
    }
    finally {
        //validateUser(false);
    }
}

function openBox(latitude, longitude) {
    let boxId = `${latitude}_${longitude}`;

    document.getElementById(boxId).classList.remove("close");
    document.getElementById(boxId).classList.add("open");
}

function validateUser(isUserEligible) {
    if (isUserEligible) {
        document.getElementById("game-baord").classList.remove("forbidden");
    }
    else {
        document.getElementById("game-baord").classList.add("forbidden");
    }
}