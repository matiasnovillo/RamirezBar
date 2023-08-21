

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

//Last modification on: 20/08/2023 23:17:57

//Create a formdata object
var formData = new FormData();

//Used for Quill Editor


//Used for file input
let ramirezbarproductphotoinput;
let ramirezbarproductphotoboolfileadded;
$("#ramirezbar-product-photo-input").on("change", function (e) {
    ramirezbarproductphotoinput = $(this).get(0).files;
    ramirezbarproductphotoboolfileadded = true;
    formData.append("ramirezbar-product-photo-input", ramirezbarproductphotoinput[0], ramirezbarproductphotoinput[0].name);
});



//LOAD EVENT
$(document).ready(function () {
    
    
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {
                
                //ProductId
                formData.append("ramirezbar-product-productid-input", $("#ramirezbar-product-productid-input").val());

                formData.append("ramirezbar-product-providerid-input", $("#ramirezbar-product-providerid-input").val());
                formData.append("ramirezbar-product-name-input", $("#ramirezbar-product-name-input").val());
                formData.append("ramirezbar-product-stock-input", $("#ramirezbar-product-stock-input").val());
                if (!ramirezbarproductphotoboolfileadded) {
                    formData.append("ramirezbar-product-photo-input", $("#ramirezbar-product-photo-readonly").val());
                }
                

                //Setup request
                var xmlHttpRequest = new XMLHttpRequest();
                //Set event listeners
                xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
                    //SAVING
                    $.notify({ message: "Saving data. Please, wait" }, { type: "info", placement: { from: "bottom", align: "center" } });
                });
                xmlHttpRequest.onload = function () {
                    if (xmlHttpRequest.status >= 400) {
                        //ERROR
                        console.log(xmlHttpRequest);
                        $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while saving the data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    }
                    else {
                        //SUCCESS
                        window.location.replace("/RamirezBar/ProductQueryPage");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/RamirezBar/Product/1/InsertOrUpdateAsync", true);
                //Send request
                xmlHttpRequest.send(formData);
            }
            else {
                $.notify({ message: "Please, complete all fields." }, { type: "warning", placement: { from: "bottom", align: "center" } });
            }


            form.classList.add("was-validated");
        }, false);
    });
});