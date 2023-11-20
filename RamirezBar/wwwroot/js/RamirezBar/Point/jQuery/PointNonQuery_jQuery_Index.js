

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
*/

//Stack: 10

//Last modification on: 06/11/2023 14:38:45

//Create a formdata object
var formData = new FormData();

//Used for Quill Editor


//Used for file input


//LOAD EVENT
$(document).ready(function () {
    
    
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if ($("#ramirezbar-point-dni-input").val() === "" || $("#ramirezbar-point-point-input").val() === "" || $("#ramirezbar-point-code-input").val() === "") {
                $.notify({ message: "Completa todos los campos, por favor" }, { type: "info", placement: { from: "bottom", align: "center" } });
                return;
            }

            if (form.checkValidity() === true) {
                
                //PointId
                formData.append("ramirezbar-point-dni-input", $("#ramirezbar-point-dni-input").val());
                formData.append("ramirezbar-point-point-input", $("#ramirezbar-point-point-input").val());
                formData.append("ramirezbar-point-code-input", $("#ramirezbar-point-code-input").val());
                

                //Setup request
                var xmlHttpRequest = new XMLHttpRequest();
                //Set event listeners
                xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
                    //SAVING
                    $.notify({ message: "Guardando..." }, { type: "info", placement: { from: "bottom", align: "center" } });
                    $("#ramirezbar-point-dni-input").val("");
                    $("#ramirezbar-point-point-input").val("");
                    $("#ramirezbar-point-code-input").val("");
                });
                xmlHttpRequest.onload = function () {
                    if (xmlHttpRequest.status >= 400) {
                        //ERROR
                        console.log(xmlHttpRequest);
                        $.notify({ icon: "fas fa-exclamation-triangle", message: "Hubo un error" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    }
                    else {
                        if (xmlHttpRequest.response === "Codigo incorrecto") {
                            $.notify({ icon: "fas fa-exclamation-triangle", message: "Codigo incorrecto" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                        }
                        else {
                            //SUCCESS
                            $.notify({ icon: "fas fa-check", message: "Puntos guardados correctamente." }, { type: "success", placement: { from: "bottom", align: "center" } });
                        }
                        
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/RamirezBar/Point/1/InsertOrUpdateAsync", true);
                //Send request
                xmlHttpRequest.send(formData);
            }
            else {
                $.notify({ message: "Por favor, completa todos los campos." }, { type: "warning", placement: { from: "bottom", align: "center" } });
            }


            form.classList.add("was-validated");
        }, false);
    });
});