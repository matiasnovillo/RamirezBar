//Import libraries to use
import { TableModel } from "../../Table/TsModels/Table_TsModel";
import { tableSelectAllPaged } from "../DTOs/tableSelectAllPaged";
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

//Last modification on: 21/08/2023 6:56:48

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

class TableQuery {
    static SelectAllPagedToHTML(request_tableSelectAllPaged: tableSelectAllPaged) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="table-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="TableId" class="btn btn-outline-secondary btn-sm" type="button">
                TableId
            </button>
        </th>
        <th scope="col">
            <button value="Name" class="btn btn-outline-secondary btn-sm" type="button">
                Nombre de mesa
            </button>
        </th>
        <th scope="col">
            <button value="UserWaiterId" class="btn btn-outline-secondary btn-sm" type="button">
                Mozo
            </button>
        </th>
        <th scope="col">
            <button value="TableStateId" class="btn btn-outline-secondary btn-sm" type="button">
                Estado
            </button>
        </th>
        <th scope="col">
            <button value="WinningMoney" class="btn btn-outline-secondary btn-sm" type="button">
                Ganancia por cliente
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        TableModel.SelectAllPaged(request_tableSelectAllPaged).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_tableQuery = newrow.response as tableSelectAllPaged;

                        //Set to default values if they are null
                        QueryString = response_tableQuery.QueryString ?? "";
                        ActualPageNumber = response_tableQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_tableQuery.RowsPerPage ?? 0;
                        SorterColumn = response_tableQuery.SorterColumn ?? "";
                        SortToggler = response_tableQuery.SortToggler ?? false;
                        TotalRows = response_tableQuery.TotalRows ?? 0;
                        TotalPages = response_tableQuery.TotalPages ?? 0;

                        //Query string
                        $("#ramirezbar-table-query-string").attr("placeholder", `Search... (${TotalRows} records)`);
                        //Total pages of pagination
                        $("#ramirezbar-table-total-pages-lg, #ramirezbar-table-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#ramirezbar-table-actual-page-number-lg, #ramirezbar-table-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#ramirezbar-table-lnk-next-page-lg, #ramirezbar-table-lnk-next-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-lnk-last-page-lg, #ramirezbar-table-lnk-last-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-search-more-button-in-list").html("");
                        }
                        else {
                            $("#ramirezbar-table-lnk-next-page-lg, #ramirezbar-table-lnk-next-page").removeAttr("disabled");
                            $("#ramirezbar-table-lnk-last-page-lg, #ramirezbar-table-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#ramirezbar-table-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#ramirezbar-table-lnk-previous-page-lg, #ramirezbar-table-lnk-previous-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-lnk-first-page-lg, #ramirezbar-table-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#ramirezbar-table-lnk-previous-page-lg, #ramirezbar-table-lnk-previous-page").removeAttr("disabled");
                            $("#ramirezbar-table-lnk-first-page-lg, #ramirezbar-table-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_tableQuery?.lstTableModel?.length === 0) {
                            $("#ramirezbar-table-lnk-previous-page-lg, #ramirezbar-table-lnk-previous-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-lnk-first-page-lg, #ramirezbar-table-lnk-first-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-lnk-next-page-lg, #ramirezbar-table-lnk-next-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-lnk-last-page-lg, #ramirezbar-table-lnk-last-page").attr("disabled", "disabled");
                            $("#ramirezbar-table-total-pages-lg, #ramirezbar-table-total-pages").html("1");
                            $("#ramirezbar-table-actual-page-number-lg, #ramirezbar-table-actual-page-number").html("1");
                        }
                        //Read data book
                        response_tableQuery?.lstTableModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="table-table-checkbox-for-row" value="${row.TableId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.TableId}
    </td>
    <td class="text-left">
        <strong><i class="fas fa-font">
            </i> ${row.Name}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.UserWaiterFantasyName}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.TableStateName}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-divide">
            </i> ${row.WinningMoney}
        </strong>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <a class="btn btn-icon-only text-primary" href="/RamirezBar/TableNonQueryPage?TableId=${row.TableId}" role="button" data-toggle="tooltip" data-original-title="Edit">
            <i class="fas fa-edit"></i>
        </a>
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger ramirezbar-table-table-delete-button" value="${row.TableId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
        <div class="dropdown">
            <button class="btn btn-sm btn-icon-only text-primary" href="#" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-ellipsis-v"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button type="button" class="dropdown-item ramirezbar-table-table-copy-button" value="${row.TableId}">
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
                        <span class="text-white text-light mb-4">
                           TableId <i class="fas fa-key"></i> ${row.TableId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Nombre de mesa <i class="fas fa-font"></i> ${row.Name}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Mozo encargado <i class="fas fa-key"></i> ${row.UserWaiterFantasyName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Estado <i class="fas fa-key"></i> ${row.TableStateName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                            WinningMoney <i class="fas fa-divide"></i> ${row.WinningMoney}
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
                                <a class="ramirezbar-table-checkbox-list list-row-unchecked icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="Check">
                                    <i class="fas fa-circle text-white"></i>
                                </a>
                                <input type="hidden" value="${row.TableId}"/>
                            </div>
                            <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="/RamirezBar/TableNonQueryPage?TableId=${row.TableId}" role="button" data-toggle="tooltip" data-original-title="edit">
                                <i class="fas fa-edit text-primary"></i>
                            </a>
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.TableId}" class="dropdown-item text-primary ramirezbar-table-list-copy-button" type="button">
                                        <i class="fas fa-copy"></i>&nbsp;Copy
                                    </button>
                                    <button value="${row.TableId}" class="dropdown-item text-danger ramirezbar-table-list-delete-button" type="button">
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
                            $("#ramirezbar-table-body-and-head-table").html("");
                            $("#ramirezbar-table-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#ramirezbar-table-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#ramirezbar-table-body-list").html("");
                                $("#ramirezbar-table-body-list").html(ListContent);
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
                    $(".ramirezbar-table-checkbox-list").on("click", function (e) {
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
                    $("#table-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.table-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.table-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.table-table-checkbox-for-row").attr("checked", "checked");
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
                    $("div.dropdown-menu button.ramirezbar-table-table-delete-button, div.dropdown-menu button.ramirezbar-table-list-delete-button").on("click", function (e) {
                        let TableId = $(this).val();
                        TableModel.DeleteByTableId(TableId).subscribe({
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
                    $("div.dropdown-menu button.ramirezbar-table-table-copy-button, div.dropdown-menu button.ramirezbar-table-list-copy-button").on("click", function (e) {
                        let TableId = $(this).val();
                        TableModel.CopyByTableId(TableId).subscribe({
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

    var _tableSelectAllPaged: tableSelectAllPaged = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    TableQuery.SelectAllPagedToHTML(_tableSelectAllPaged);
}

//LOAD EVENT
if ($("#ramirezbar-table-title-page").html().includes("Query table")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "TableId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#ramirezbar-table-lnk-first-page-lg, #ramirezbar-table-lnk-first-page").attr("disabled", "disabled");
    $("#ramirezbar-table-lnk-previous-page-lg, #ramirezbar-table-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#ramirezbar-table-export-message").html("");

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#ramirezbar-table-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#ramirezbar-table-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#ramirezbar-table-lnk-first-page-lg, #ramirezbar-table-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#ramirezbar-table-lnk-previous-page-lg, #ramirezbar-table-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#ramirezbar-table-lnk-next-page-lg, #ramirezbar-table-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#ramirezbar-table-lnk-last-page-lg, #ramirezbar-table-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#ramirezbar-table-table-view-button").on("click", function (e) {
    $("#ramirezbar-table-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#ramirezbar-table-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#ramirezbar-table-list-view-button").on("click", function (e) {
    $("#ramirezbar-table-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#ramirezbar-table-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#ramirezbar-table-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#ramirezbar-table-search-more-button-in-list").offset()?.top ?? 0) + ($("#ramirezbar-table-search-more-button-in-list").outerHeight() ?? 0);

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
$("#ramirezbar-table-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#ramirezbar-table-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.table-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/RamirezBar/Table/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#ramirezbar-table-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for PDF file
            $("#ramirezbar-table-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/RamirezBar/Table/Table_${DateTimeNow.AjaxForString}.pdf" type="button" download>
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
$("#ramirezbar-table-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#ramirezbar-table-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.table-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/RamirezBar/Table/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#ramirezbar-table-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for Excel file
            $("#ramirezbar-table-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/RamirezBar/Table/Table_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
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
$("#ramirezbar-table-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#ramirezbar-table-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.table-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/RamirezBar/Table/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#ramirezbar-table-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for CSV file
            $("#ramirezbar-table-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/RamirezBar/Table/Table_${DateTimeNow.AjaxForString}.csv" type="button" download>
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
$("#ramirezbar-table-export-close-button").on("click", function (e) {
    $("#ramirezbar-table-export-message").html("");
});

//Massive action Copy
$("#ramirezbar-table-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#ramirezbar-table-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.table-table-checkbox-for-row:checked").each(function () {
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

    TableModel.CopyManyOrAll(CopyType, Body).subscribe({
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
$("#ramirezbar-table-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#ramirezbar-table-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.table-table-checkbox-for-row:checked").each(function () {
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

    TableModel.DeleteManyOrAll(DeleteType, Body).subscribe({
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