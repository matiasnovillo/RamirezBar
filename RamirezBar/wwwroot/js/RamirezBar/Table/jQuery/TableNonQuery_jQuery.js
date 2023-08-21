

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

//Last modification on: 21/08/2023 6:56:48

//Create a formdata object
var formData = new FormData();

//Used for Quill Editor


//Used for file input
let ramirezbartablephotoinput;
let ramirezbartablephotoboolfileadded;
$("#ramirezbar-table-photo-input").on("change", function (e) {
    ramirezbartablephotoinput = $(this).get(0).files;
    ramirezbartablephotoboolfileadded = true;
    formData.append("ramirezbar-table-photo-input", ramirezbartablephotoinput[0], ramirezbartablephotoinput[0].name);
});



//LOAD EVENT
$(document).ready(function () {
    //UserWaiterId select tag
    $("#ramirezbar-table-userwaiterid-select").on("change", function (e) {
        $("#ramirezbar-table-userwaiterid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#ramirezbar-table-userwaiterid-select option:selected").text()}
            </a>
            <input type="hidden" id="ramirezbar-table-userwaiterid-input" value="${$("#ramirezbar-table-userwaiterid-select option:selected").val()}"/>
        </li>`);
    });

    //TableStateId select tag
    $("#ramirezbar-table-tablestateid-select").on("change", function (e) {
        $("#ramirezbar-table-tablestateid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#ramirezbar-table-tablestateid-select option:selected").text()}
            </a>
            <input type="hidden" id="ramirezbar-table-tablestateid-input" value="${$("#ramirezbar-table-tablestateid-select option:selected").val()}"/>
        </li>`);
    });
    
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {
                
                //TableId
                formData.append("ramirezbar-table-tableid-input", $("#ramirezbar-table-tableid-input").val());

                formData.append("ramirezbar-table-name-input", $("#ramirezbar-table-name-input").val());
                if (!ramirezbartablephotoboolfileadded) {
                    formData.append("ramirezbar-table-photo-input", $("#ramirezbar-table-photo-readonly").val());
                }
                formData.append("ramirezbar-table-userwaiterid-input", $("#ramirezbar-table-userwaiterid-input").val());
                formData.append("ramirezbar-table-tablestateid-input", $("#ramirezbar-table-tablestateid-input").val());
                formData.append("ramirezbar-table-winningmoney-input", $("#ramirezbar-table-winningmoney-input").val());
                

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
                        window.location.replace("/RamirezBar/TableQueryPage");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/RamirezBar/Table/1/InsertOrUpdateAsync", true);
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