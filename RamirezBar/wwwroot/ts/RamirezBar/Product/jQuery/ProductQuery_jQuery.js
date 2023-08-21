"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//Import libraries to use
var Product_TsModel_1 = require("../../Product/TsModels/Product_TsModel");
var $ = require("jquery");
var Rx = require("rxjs");
var ajax_1 = require("rxjs/ajax");
require("bootstrap-notify");
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
var LastTopDistance = 0;
var QueryString = "";
var ActualPageNumber = 1;
var RowsPerPage = 50;
var SorterColumn = "";
var SortToggler = false;
var TotalPages = 0;
var TotalRows = 0;
var ViewToggler = "List";
var ScrollDownNSearchFlag = false;
var ProductQuery = /** @class */ (function () {
    function ProductQuery() {
    }
    ProductQuery.SelectAllPagedToHTML = function (request_productSelectAllPaged) {
        //Used for list view
        $(window).off("scroll");
        //Load some part of table
        var TableContent = "<thead class=\"thead-light\">\n    <tr>\n        <th scope=\"col\">\n            <div>\n                <input id=\"product-table-check-all\" type=\"checkbox\">\n            </div>\n        </th>\n        <th scope=\"col\">\n            <button value=\"ProductId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                ProductId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Name\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Nombre\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"ProviderId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Proveedor\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Stock\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Stock\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Price\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Precio\n            </button>\n        </th>\n        \n        <th scope=\"col\"></th>\n    </tr>\n</thead>\n<tbody>";
        var ListContent = "";
        Product_TsModel_1.ProductModel.SelectAllPaged(request_productSelectAllPaged).subscribe({
            next: function (newrow) {
                var _a, _b, _c, _d, _e, _f, _g, _h, _j;
                //Only works when there is data available
                if (newrow.status != 204) {
                    var response_productQuery = newrow.response;
                    //Set to default values if they are null
                    QueryString = (_a = response_productQuery.QueryString) !== null && _a !== void 0 ? _a : "";
                    ActualPageNumber = (_b = response_productQuery.ActualPageNumber) !== null && _b !== void 0 ? _b : 0;
                    RowsPerPage = (_c = response_productQuery.RowsPerPage) !== null && _c !== void 0 ? _c : 0;
                    SorterColumn = (_d = response_productQuery.SorterColumn) !== null && _d !== void 0 ? _d : "";
                    SortToggler = (_e = response_productQuery.SortToggler) !== null && _e !== void 0 ? _e : false;
                    TotalRows = (_f = response_productQuery.TotalRows) !== null && _f !== void 0 ? _f : 0;
                    TotalPages = (_g = response_productQuery.TotalPages) !== null && _g !== void 0 ? _g : 0;
                    //Query string
                    $("#ramirezbar-product-query-string").attr("placeholder", "Search... (".concat(TotalRows, " records)"));
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
                    if (((_h = response_productQuery === null || response_productQuery === void 0 ? void 0 : response_productQuery.lstProductModel) === null || _h === void 0 ? void 0 : _h.length) === 0) {
                        $("#ramirezbar-product-lnk-previous-page-lg, #ramirezbar-product-lnk-previous-page").attr("disabled", "disabled");
                        $("#ramirezbar-product-lnk-first-page-lg, #ramirezbar-product-lnk-first-page").attr("disabled", "disabled");
                        $("#ramirezbar-product-lnk-next-page-lg, #ramirezbar-product-lnk-next-page").attr("disabled", "disabled");
                        $("#ramirezbar-product-lnk-last-page-lg, #ramirezbar-product-lnk-last-page").attr("disabled", "disabled");
                        $("#ramirezbar-product-total-pages-lg, #ramirezbar-product-total-pages").html("1");
                        $("#ramirezbar-product-actual-page-number-lg, #ramirezbar-product-actual-page-number").html("1");
                    }
                    //Read data book
                    (_j = response_productQuery === null || response_productQuery === void 0 ? void 0 : response_productQuery.lstProductModel) === null || _j === void 0 ? void 0 : _j.forEach(function (row) {
                        TableContent += "<tr>\n    <!-- Checkbox -->\n    <td>\n        <div>\n            <input class=\"product-table-checkbox-for-row\" value=\"".concat(row.ProductId, "\" type=\"checkbox\">\n        </div>\n    </td>\n    <!-- Data -->\n    <td class=\"text-left text-light\">\n        <i class=\"fas fa-key\"></i> ").concat(row.ProductId, "\n    </td>\n    <td class=\"text-left\">\n        <strong><i class=\"fas fa-font\">\n            </i> ").concat(row.Name, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> ").concat(row.ProviderName, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong><i class=\"fas fa-divide\">\n            </i> ").concat(row.Stock, "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong><i class=\"fas fa-divide\">\n            </i> ").concat(row.Price, "\n        </strong>\n    </td>\n    \n    <!-- Actions -->\n    <td class=\"text-right\">\n        <a class=\"btn btn-icon-only text-primary\" href=\"/RamirezBar/ProductNonQueryPage?ProductId=").concat(row.ProductId, "\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"Edit\">\n            <i class=\"fas fa-edit\"></i>\n        </a>\n        <div class=\"dropdown\">\n            <button class=\"btn btn-icon-only text-danger\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                <i class=\"fas fa-trash\"></i>\n            </button>\n            <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                <button class=\"dropdown-item text-danger ramirezbar-product-table-delete-button\" value=\"").concat(row.ProductId, "\" type=\"button\">\n                    <i class=\"fas fa-exclamation-triangle\"></i> Yes, delete\n                </button>\n            </div>\n        </div>\n        <div class=\"dropdown\">\n            <button class=\"btn btn-sm btn-icon-only text-primary\" href=\"#\" type=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                <i class=\"fas fa-ellipsis-v\"></i>\n            </button>\n            <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                <button type=\"button\" class=\"dropdown-item ramirezbar-product-table-copy-button\" value=\"").concat(row.ProductId, "\">\n                    <i class=\"fas fa-copy text-primary\"></i>&nbsp;Copy\n                </button>\n            </div>\n        </div>\n    </td>\n</tr>");
                        ListContent += "<div class=\"row mx-2\">\n    <div class=\"col-sm\">\n        <div class=\"card bg-gradient-primary mb-2\">\n            <div class=\"card-body\">\n                <div class=\"row\">\n                    <div class=\"col text-truncate\">\n                        <img src=\"".concat(row.Photo, "\" alt=\"Circle image\" class=\"img-fluid rounded-circle shadow\" style=\"width: 150px;\">\n                        <br/>\n                        <span class=\"text-white text-light mb-4\">\n                           ProductId <i class=\"fas fa-key\"></i> ").concat(row.ProductId, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           Nombre <i class=\"fas fa-font\"></i> ").concat(row.Name, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           Proveedor <i class=\"fas fa-key\"></i> ").concat(row.ProviderName, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                            Stock <i class=\"fas fa-divide\"></i> ").concat(row.Stock, "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                            Precio <i class=\"fas fa-divide\"></i> ").concat(row.Price, "\n                        </span>\n                        <br/>\n                        \n                    </div>\n                    <div class=\"col-auto\">\n                    </div>\n                </div>\n                <!-- Actions -->\n                <div class=\"row\">\n                    <div class=\"col\">\n                        <div class=\"justify-content-end text-right mt-2\">\n                            <div class=\"mb-2\">\n                                <a class=\"ramirezbar-product-checkbox-list list-row-unchecked icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"Check\">\n                                    <i class=\"fas fa-circle text-white\"></i>\n                                </a>\n                                <input type=\"hidden\" value=\"").concat(row.ProductId, "\"/>\n                            </div>\n                            <a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"/RamirezBar/ProductNonQueryPage?ProductId=").concat(row.ProductId, "\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"edit\">\n                                <i class=\"fas fa-edit text-primary\"></i>\n                            </a>\n                            <div class=\"dropup\">\n                                <a class=\"icon icon-shape bg-white icon-sm text-primary rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                                    <i class=\"fas fa-ellipsis-v\"></i>\n                                </a>\n                                <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                                    <button value=\"").concat(row.ProductId, "\" class=\"dropdown-item text-primary ramirezbar-product-list-copy-button\" type=\"button\">\n                                        <i class=\"fas fa-copy\"></i>&nbsp;Copy\n                                    </button>\n                                    <button value=\"").concat(row.ProductId, "\" class=\"dropdown-item text-danger ramirezbar-product-list-delete-button\" type=\"button\">\n                                        <i class=\"fas fa-trash\"></i>&nbsp;Delete\n                                    </button>\n                                </div>\n                            </div>\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n</div>");
                    });
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
            complete: function () {
                //Execute ScrollDownNSearch function when the user scroll the page
                $(window).on("scroll", ScrollDownNSearch);
                //Add final content to TableContent
                TableContent += "</tbody>\n                                </table>";
                //Check button inside list view
                $(".ramirezbar-product-checkbox-list").on("click", function (e) {
                    //Toggler
                    if ($(this).hasClass("list-row-checked")) {
                        $(this).html("<a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                                            <i class=\"fas fa-circle text-white\"></i>\n                                                        </a>");
                        $(this).removeClass("list-row-checked").addClass("list-row-unchecked");
                    }
                    else {
                        $(this).html("<a class=\"icon icon-shape bg-white icon-sm text-primary rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                                            <i class=\"fas fa-check\"></i>\n                                                        </a>");
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
                    var ProductId = $(this).val();
                    Product_TsModel_1.ProductModel.DeleteByProductId(ProductId).subscribe({
                        next: function (newrow) {
                        },
                        complete: function () {
                            //SUCCESS
                            // @ts-ignore
                            $.notify({ icon: "fas fa-check", message: "Row deleted successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });
                            ValidateAndSearch();
                        },
                        error: function (err) {
                            //ERROR
                            // @ts-ignore
                            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                            console.log(err);
                        }
                    });
                });
                //Copy button in table and list
                $("div.dropdown-menu button.ramirezbar-product-table-copy-button, div.dropdown-menu button.ramirezbar-product-list-copy-button").on("click", function (e) {
                    var ProductId = $(this).val();
                    Product_TsModel_1.ProductModel.CopyByProductId(ProductId).subscribe({
                        next: function (newrow) {
                        },
                        complete: function () {
                            //SUCCESS
                            // @ts-ignore
                            $.notify({ icon: "fas fa-check", message: "Row copied successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });
                            ValidateAndSearch();
                        },
                        error: function (err) {
                            //ERROR
                            // @ts-ignore
                            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                            console.log(err);
                        }
                    });
                });
            },
            error: function (err) {
                //ERROR
                // @ts-ignore
                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to get data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                console.log(err);
            }
        });
    };
    return ProductQuery;
}());
function ValidateAndSearch() {
    var _productSelectAllPaged = {
        QueryString: QueryString,
        ActualPageNumber: ActualPageNumber,
        RowsPerPage: RowsPerPage,
        SorterColumn: SorterColumn,
        SortToggler: SortToggler,
        TotalRows: TotalRows,
        TotalPages: TotalPages
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
    var _a, _b;
    //If undefined, set QueryString to "" value
    QueryString = (_b = ((_a = $(this).val()) === null || _a === void 0 ? void 0 : _a.toString())) !== null && _b !== void 0 ? _b : "";
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
    var _a, _b, _c, _d, _e, _f, _g, _h;
    var WindowsTopDistance = (_a = $(window).scrollTop()) !== null && _a !== void 0 ? _a : 0;
    var WindowsBottomDistance = ((_b = $(window).scrollTop()) !== null && _b !== void 0 ? _b : 0) + ((_c = $(window).innerHeight()) !== null && _c !== void 0 ? _c : 0);
    var CardsFooterTopPosition = (_e = (_d = $("#ramirezbar-product-search-more-button-in-list").offset()) === null || _d === void 0 ? void 0 : _d.top) !== null && _e !== void 0 ? _e : 0;
    var CardsFooterBottomPosition = ((_g = (_f = $("#ramirezbar-product-search-more-button-in-list").offset()) === null || _f === void 0 ? void 0 : _f.top) !== null && _g !== void 0 ? _g : 0) + ((_h = $("#ramirezbar-product-search-more-button-in-list").outerHeight()) !== null && _h !== void 0 ? _h : 0);
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
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#ramirezbar-product-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_1 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows_1.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_1.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_1.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_1.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/RamirezBar/Product/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#ramirezbar-product-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });
            //Show download button for PDF file
            $("#ramirezbar-product-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/PDFFiles/RamirezBar/Product/Product_".concat(DateTimeNow.AjaxForString, ".pdf\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-pdf\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>"));
        },
        error: function (err) {
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
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#ramirezbar-product-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_2 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows_2.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_2.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_2.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_2.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/RamirezBar/Product/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#ramirezbar-product-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });
            //Show download button for Excel file
            $("#ramirezbar-product-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/ExcelFiles/RamirezBar/Product/Product_".concat(DateTimeNow.AjaxForString, ".xlsx\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-excel\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>"));
        },
        error: function (err) {
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
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#ramirezbar-product-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_3 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows_3.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_3.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_3.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_3.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/RamirezBar/Product/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#ramirezbar-product-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });
            //Show download button for CSV file
            $("#ramirezbar-product-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/CSVFiles/RamirezBar/Product/Product_".concat(DateTimeNow.AjaxForString, ".csv\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-csv\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>"));
        },
        error: function (err) {
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
    var CopyType = "";
    var Body = {};
    if ($("#ramirezbar-product-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        var CheckedRows_4 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows_4.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_4.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows_4.toString()
        };
    }
    Product_TsModel_1.ProductModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed copy" }, { type: "success", placement: { from: "bottom", align: "center" } });
            ValidateAndSearch();
        },
        error: function (err) {
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
    var DeleteType = "";
    var Body = {};
    if ($("#ramirezbar-product-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        var CheckedRows_5 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.product-table-checkbox-for-row:checked").each(function () {
                CheckedRows_5.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_5.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows_5.toString()
        };
    }
    Product_TsModel_1.ProductModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed deletion" }, { type: "success", placement: { from: "bottom", align: "center" } });
            ValidateAndSearch();
        },
        error: function (err) {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});
//# sourceMappingURL=ProductQuery_jQuery.js.map