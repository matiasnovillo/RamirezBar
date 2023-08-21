//Import libraries to use
import { ProductModel } from "../../Product/TsModels/Product_TsModel";
import { productSelectAllPaged } from "../DTOs/productSelectAllPaged";
import * as $ from "jquery";
import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import "bootstrap-notify";

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

//Last modification on: 21/08/2023 6:49:20

//Set default values
let LastTopDistance: number = 0;
let QueryString: string = "";
let ActualPageNumber: number = 1;
let RowsPerPage: number = 50;
let SorterColumn: string | undefined = "";
let SortToggler: boolean = false;
let TotalPages: number = 0;
let TotalRows: number = 0;
let ViewToggler: string = "List";
let ScrollDownNSearchFlag: boolean = false;

class ProductQuery {
    static SelectAllPagedToHTML(request_productSelectAllPaged: productSelectAllPaged) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="product-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="ProductId" class="btn btn-outline-secondary btn-sm" type="button">
                ProductId
            </button>
        </th>
        <th scope="col">
            <button value="Name" class="btn btn-outline-secondary btn-sm" type="button">
                Nombre
            </button>
        </th>
        <th scope="col">
            <button value="ProviderId" class="btn btn-outline-secondary btn-sm" type="button">
                Proveedor
            </button>
        </th>
        <th scope="col">
            <button value="Stock" class="btn btn-outline-secondary btn-sm" type="button">
                Stock
            </button>
        </th>
        <th scope="col">
            <button value="Price" class="btn btn-outline-secondary btn-sm" type="button">
                Precio
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        ProductModel.SelectAllPaged(request_productSelectAllPaged).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_productQuery = newrow.response as productSelectAllPaged;

                        //Set to default values if they are null
                        QueryString = response_productQuery.QueryString ?? "";
                        ActualPageNumber = response_productQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_productQuery.RowsPerPage ?? 0;
                        SorterColumn = response_productQuery.SorterColumn ?? "";
                        SortToggler = response_productQuery.SortToggler ?? false;
                        TotalRows = response_productQuery.TotalRows ?? 0;
                        TotalPages = response_productQuery.TotalPages ?? 0;

                        //Query string
                        $("#ramirezbar-product-query-string").attr("placeholder", `Search... (${TotalRows} records)`);
                        //Total pages of pagination
                        $("#ramirezbar-product-total-pages-lg, #ramirezbar-product-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#ramirezbar-product-actual-page-number-lg, #ramirezbar-product-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#ramirezbar-product-lnk-next-page-lg, #ramirezbar-product-lnk-next-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-lnk-last-page-lg, #ramirezbar-product-lnk-last-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-search-more-button-in-list").html("");
                        }
                        else {
                            $("#ramirezbar-product-lnk-next-page-lg, #ramirezbar-product-lnk-next-page").removeAttr("disabled");
                            $("#ramirezbar-product-lnk-last-page-lg, #ramirezbar-product-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#ramirezbar-product-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#ramirezbar-product-lnk-previous-page-lg, #ramirezbar-product-lnk-previous-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-lnk-first-page-lg, #ramirezbar-product-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#ramirezbar-product-lnk-previous-page-lg, #ramirezbar-product-lnk-previous-page").removeAttr("disabled");
                            $("#ramirezbar-product-lnk-first-page-lg, #ramirezbar-product-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_productQuery?.lstProductModel?.length === 0) {
                            $("#ramirezbar-product-lnk-previous-page-lg, #ramirezbar-product-lnk-previous-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-lnk-first-page-lg, #ramirezbar-product-lnk-first-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-lnk-next-page-lg, #ramirezbar-product-lnk-next-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-lnk-last-page-lg, #ramirezbar-product-lnk-last-page").attr("disabled", "disabled");
                            $("#ramirezbar-product-total-pages-lg, #ramirezbar-product-total-pages").html("1");
                            $("#ramirezbar-product-actual-page-number-lg, #ramirezbar-product-actual-page-number").html("1");
                        }
                        //Read data book
                        response_productQuery?.lstProductModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="product-table-checkbox-for-row" value="${row.ProductId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.ProductId}
    </td>
    <td class="text-left">
        <strong><i class="fas fa-font">
            </i> ${row.Name}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.ProviderName}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-divide">
            </i> ${row.Stock}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-divide">
            </i> ${row.Price}
        </strong>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <a class="btn btn-icon-only text-primary" href="/RamirezBar/ProductNonQueryPage?ProductId=${row.ProductId}" role="button" data-toggle="tooltip" data-original-title="Edit">
            <i class="fas fa-edit"></i>
        </a>
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger ramirezbar-product-table-delete-button" value="${row.ProductId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
        <div class="dropdown">
            <button class="btn btn-sm btn-icon-only text-primary" href="#" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-ellipsis-v"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button type="button" class="dropdown-item ramirezbar-product-table-copy-button" value="${row.ProductId}">
                    <i class="fas fa-copy text-primary"></i>&nbsp;Copy
                </button>
            </div>
        </div>
    </td>
</tr>`;

                            ListContent += `<div class="row mx-2">
    <div class="col-sm">
        <div class="card bg-gradient-primary mb-2">
            <div class="card-body">
                <div class="row">
                    <div class="col text-truncate">
                        <img src="${row.Photo}" alt="Circle image" class="img-fluid rounded-circle shadow" style="width: 150px;">
                        <br/>
                        <span class="text-white text-light mb-4">
                           ProductId <i class="fas fa-key"></i> ${row.ProductId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Nombre <i class="fas fa-font"></i> ${row.Name}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Proveedor <i class="fas fa-key"></i> ${row.ProviderName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                            Stock <i class="fas fa-divide"></i> ${row.Stock}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                            Precio <i class="fas fa-divide"></i> ${row.Price}
                        </span>
                        <br/>
                        
                    </div>
                    <div class="col-auto">
                    </div>
                </div>
                <!-- Actions -->
                <div class="row">
                    <div class="col">
                        <div class="justify-content-end text-right mt-2">
                            <div class="mb-2">
                                <a class="ramirezbar-product-checkbox-list list-row-unchecked icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="Check">
                                    <i class="fas fa-circle text-white"></i>
                                </a>
                                <input type="hidden" value="${row.ProductId}"/>
                            </div>
                            <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="/RamirezBar/ProductNonQueryPage?ProductId=${row.ProductId}" role="button" data-toggle="tooltip" data-original-title="edit">
                                <i class="fas fa-edit text-primary"></i>
                            </a>
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.ProductId}" class="dropdown-item text-primary ramirezbar-product-list-copy-button" type="button">
                                        <i class="fas fa-copy"></i>&nbsp;Copy
                                    </button>
                                    <button value="${row.ProductId}" class="dropdown-item text-danger ramirezbar-product-list-delete-button" type="button">
                                        <i class="fas fa-trash"></i>&nbsp;Delete
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>`;
                        })

                        //If view table is activated, clear table view, if not, clear list view
                        if (ViewToggler === "Table") {
                            $("#ramirezbar-product-body-and-head-table").html("");
                            $("#ramirezbar-product-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#ramirezbar-product-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#ramirezbar-product-body-list").html("");
                                $("#ramirezbar-product-body-list").html(ListContent);
                            }
                            }
                    }
                    else {
                        //ERROR
                        // @ts-ignore
                        $.notify({ icon: "fas fa-exclamation-triangle", message: "No registers found" }, { type: "warning", placement: { from: "bottom", align: "center" } });
                    }
                },
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Add final content to TableContent
                    TableContent += `</tbody>
                                </table>`;

                    //Check button inside list view
                    $(".ramirezbar-product-checkbox-list").on("click", function (e) {
                        //Toggler
                        if ($(this).hasClass("list-row-checked")) {
                            $(this).html(`<a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                                            <i class="fas fa-circle text-white"></i>
                                                        </a>`);
                            $(this).removeClass("list-row-checked").addClass("list-row-unchecked");
                        }
                        else {
                            $(this).html(`<a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                                            <i class="fas fa-check"></i>
                                                        </a>`);
                            $(this).removeClass("list-row-unchecked").addClass("list-row-checked");
                        }
                    });

                    //Check all button inside table
                    $("#product-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.product-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.product-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.product-table-checkbox-for-row").attr("checked", "checked");
                        }
                    });

                    //Buttons inside head of table
                    $("tr th button").one("click", function (e) {
                        //Toggler
                        if (SorterColumn == $(this).attr("value")) {
                            SorterColumn = "";
                            SortToggler = true;
                        }
                        else {
                            SorterColumn = $(this).attr("value");
                            SortToggler = false;
                        }

                        ValidateAndSearch();
                    });

                    //Delete button in table and list
                    $("div.dropdown-menu button.ramirezbar-product-table-delete-button, div.dropdown-menu button.ramirezbar-product-list-delete-button").on("click", function (e) {
                        let ProductId = $(this).val();
                        ProductModel.DeleteByProductId(ProductId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                //SUCCESS
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Row deleted successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                                ValidateAndSearch();
                            },
                            error: err => {
                                //ERROR
                                // @ts-ignore
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                                console.log(err);
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.ramirezbar-product-table-copy-button, div.dropdown-menu button.ramirezbar-product-list-copy-button").on("click", function (e) {
                        let ProductId = $(this).val();
                        ProductModel.CopyByProductId(ProductId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                //SUCCESS
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Row copied successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                                ValidateAndSearch();
                            },
                            error: err => {
                                //ERROR
                                // @ts-ignore
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                                console.log(err);
                            }
                        });
                    });
                },
                error: err => {
                    //ERROR
                    // @ts-ignore
                    $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to get data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    console.log(err);
                }
            });
    }
}

function ValidateAndSearch() {

    var _productSelectAllPaged: productSelectAllPaged = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    ProductQuery.SelectAllPagedToHTML(_productSelectAllPaged);
}

//LOAD EVENT
if ($("#ramirezbar-product-title-page").html().includes("Query product")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "ProductId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#ramirezbar-product-lnk-first-page-lg, #ramirezbar-product-lnk-first-page").attr("disabled", "disabled");
    $("#ramirezbar-product-lnk-previous-page-lg, #ramirezbar-product-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#ramirezbar-product-export-message").html("");

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#ramirezbar-product-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#ramirezbar-product-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#ramirezbar-product-lnk-first-page-lg, #ramirezbar-product-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#ramirezbar-product-lnk-previous-page-lg, #ramirezbar-product-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#ramirezbar-product-lnk-next-page-lg, #ramirezbar-product-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#ramirezbar-product-lnk-last-page-lg, #ramirezbar-product-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#ramirezbar-product-table-view-button").on("click", function (e) {
    $("#ramirezbar-product-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#ramirezbar-product-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#ramirezbar-product-list-view-button").on("click", function (e) {
    $("#ramirezbar-product-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#ramirezbar-product-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#ramirezbar-product-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#ramirezbar-product-search-more-button-in-list").offset()?.top ?? 0) + ($("#ramirezbar-product-search-more-button-in-list").outerHeight() ?? 0);

    if (WindowsTopDistance > LastTopDistance) {
        //Scroll down
        if ((WindowsBottomDistance > CardsFooterTopPosition) && (WindowsTopDistance < CardsFooterBottomPosition)) {
            //Search More button visible
            if (ActualPageNumber !== TotalPages) {
                ScrollDownNSearchFlag = true;
                ActualPageNumber += 1;
                ValidateAndSearch();
            }
        }
        else { /*Card footer not visible*/ }
    }
    else { /*Scroll up*/ }
    LastTopDistance = WindowsTopDistance;
}

//Used to list view
$(window).on("scroll", ScrollDownNSearch);

//Export as PDF button
$("#ramirezbar-product-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#ramirezbar-product-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
    }

    Rx.from(ajax.post("/api/RamirezBar/Product/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#ramirezbar-product-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for PDF file
            $("#ramirezbar-product-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/RamirezBar/Product/Product_${DateTimeNow.AjaxForString}.pdf" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-pdf"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Export as Excel button
$("#ramirezbar-product-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#ramirezbar-product-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
    }

    Rx.from(ajax.post("/api/RamirezBar/Product/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#ramirezbar-product-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for Excel file
            $("#ramirezbar-product-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/RamirezBar/Product/Product_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-excel"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Export as CSV button
$("#ramirezbar-product-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#ramirezbar-product-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
    }

    Rx.from(ajax.post("/api/RamirezBar/Product/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#ramirezbar-product-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for CSV file
            $("#ramirezbar-product-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/RamirezBar/Product/Product_${DateTimeNow.AjaxForString}.csv" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-csv"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Export close button in modal
$("#ramirezbar-product-export-close-button").on("click", function (e) {
    $("#ramirezbar-product-export-message").html("");
});

//Massive action Copy
$("#ramirezbar-product-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#ramirezbar-product-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows.toString()
        };
    }

    ProductModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed copy" }, { type: "success", placement: { from: "bottom", align: "center" } });

            ValidateAndSearch();
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Massive action Delete
$("#ramirezbar-product-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#ramirezbar-product-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows.toString()
        };
    }

    ProductModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed deletion" }, { type: "success", placement: { from: "bottom", align: "center" } });

            ValidateAndSearch();
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});