"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TableModel = void 0;
var Rx = require("rxjs");
var ajax_1 = require("rxjs/ajax");
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
//11 fields | Sub-models: 0 models  | Last modification on: 21/08/2023 6:56:48 | Stack: 9
var TableModel = /** @class */ (function () {
    function TableModel() {
    }
    //Queries
    TableModel.Select1ByTableId = function (TableId) {
        var URL = "/api/RamirezBar/Table/1/Select1ByTableIdToJSON/" + TableId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TableModel.SelectAll = function () {
        var URL = "/api/RamirezBar/Table/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TableModel.SelectAllPaged = function (tableSelectAllPaged) {
        var URL = "/api/RamirezBar/Table/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: tableSelectAllPaged.QueryString,
            ActualPageNumber: tableSelectAllPaged.ActualPageNumber,
            RowsPerPage: tableSelectAllPaged.RowsPerPage,
            SorterColumn: tableSelectAllPaged.SorterColumn,
            SortToggler: tableSelectAllPaged.SortToggler,
            RowCount: tableSelectAllPaged.TotalRows,
            TotalPages: tableSelectAllPaged.TotalPages,
            lstTableModel: tableSelectAllPaged.lstTableModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    TableModel.DeleteByTableId = function (TableId) {
        var URL = "/api/RamirezBar/Table/1/DeleteByTableId/" + TableId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    TableModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/Table/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TableModel.CopyByTableId = function (TableId) {
        var URL = "/api/RamirezBar/Table/1/CopyByTableId/" + TableId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TableModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Tableing/Table/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return TableModel;
}());
exports.TableModel = TableModel;
//# sourceMappingURL=Table_TsModel.js.map