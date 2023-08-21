//Create a formdata object
var formData = new FormData();

$("#logout-button").on("click", function (e) {
    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
    });
    xmlHttpRequest.upload.addEventListener("progress", function (e) {
        // While sending and loading data.
    });
    xmlHttpRequest.upload.addEventListener("load", function (e) {
        // When the request has successfully completed.
    });
    xmlHttpRequest.upload.addEventListener("loadend", function (e) {
        // When the request has completed (either in success or failure).
    });
    xmlHttpRequest.upload.addEventListener("error", function (e) {
        // When the request has failed.
    });
    xmlHttpRequest.upload.addEventListener("abort", function (e) {
        // When the request has been aborted. 
    });
    xmlHttpRequest.upload.addEventListener("timeout", function (e) {
        // When the author specified timeout has passed before the request could complete
    });
    xmlHttpRequest.onload = function () {
        if (xmlHttpRequest.status >= 400) {
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            window.location.href = xmlHttpRequest.response;
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/CMSCore/User/1/Logout", false);
    //Send request
    xmlHttpRequest.send(formData);
});