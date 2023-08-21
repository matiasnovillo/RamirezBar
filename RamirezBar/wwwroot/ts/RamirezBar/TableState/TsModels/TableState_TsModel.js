"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TableStateModel = void 0;
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
//7 fields | Sub-models: 1 models  | Last modification on: 21/08/2023 6:20:18 | Stack: 9
var TableStateModel = /** @class */ (function () {
    function TableStateModel() {
    }
    //Queries
    TableStateModel.Select1ByTableStateId = function (TableStateId) {
        var URL = "/api/RamirezBar/TableState/1/Select1ByTableStateIdToJSON/" + TableStateId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TableStateModel.SelectAll = function () {
        var URL = "/api/RamirezBar/TableState/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TableStateModel.SelectAllPaged = function (tablestateSelectAllPaged) {
        var URL = "/api/RamirezBar/TableState/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: tablestateSelectAllPaged.QueryString,
            ActualPageNumber: tablestateSelectAllPaged.ActualPageNumber,
            RowsPerPage: tablestateSelectAllPaged.RowsPerPage,
            SorterColumn: tablestateSelectAllPaged.SorterColumn,
            SortToggler: tablestateSelectAllPaged.SortToggler,
            RowCount: tablestateSelectAllPaged.TotalRows,
            TotalPages: tablestateSelectAllPaged.TotalPages,
            lstTableStateModel: tablestateSelectAllPaged.lstTableStateModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    TableStateModel.DeleteByTableStateId = function (TableStateId) {
        var URL = "/api/RamirezBar/TableState/1/DeleteByTableStateId/" + TableStateId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    TableStateModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/TableState/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TableStateModel.CopyByTableStateId = function (TableStateId) {
        var URL = "/api/RamirezBar/TableState/1/CopyByTableStateId/" + TableStateId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TableStateModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/TableStateing/TableState/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return TableStateModel;
}());
exports.TableStateModel = TableStateModel;
//# sourceMappingURL=TableState_TsModel.js.map