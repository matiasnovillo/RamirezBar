

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

//Last modification on: 20/08/2023 23:02:42

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

            if (form.checkValidity() === true) {
                
                //ProductCategoryId
                formData.append("ramirezbar-productcategory-productcategoryid-input", $("#ramirezbar-productcategory-productcategoryid-input").val());

                formData.append("ramirezbar-productcategory-name-input", $("#ramirezbar-productcategory-name-input").val());
                

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
                        window.location.replace("/RamirezBar/ProductCategoryQueryPage");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/RamirezBar/ProductCategory/1/InsertOrUpdateAsync", true);
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