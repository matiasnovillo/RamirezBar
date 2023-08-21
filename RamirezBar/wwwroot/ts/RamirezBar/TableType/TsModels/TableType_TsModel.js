"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TableTypeModel = void 0;
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
//7 fields | Sub-models: 0 models  | Last modification on: 20/08/2023 23:03:00 | Stack: 9
var TableTypeModel = /** @class */ (function () {
    function TableTypeModel() {
    }
    //Queries
    TableTypeModel.Select1ByTableTypeId = function (TableTypeId) {
        var URL = "/api/RamirezBar/TableType/1/Select1ByTableTypeIdToJSON/" + TableTypeId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TableTypeModel.SelectAll = function () {
        var URL = "/api/RamirezBar/TableType/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TableTypeModel.SelectAllPaged = function (tabletypeSelectAllPaged) {
        var URL = "/api/RamirezBar/TableType/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: tabletypeSelectAllPaged.QueryString,
            ActualPageNumber: tabletypeSelectAllPaged.ActualPageNumber,
            RowsPerPage: tabletypeSelectAllPaged.RowsPerPage,
            SorterColumn: tabletypeSelectAllPaged.SorterColumn,
            SortToggler: tabletypeSelectAllPaged.SortToggler,
            RowCount: tabletypeSelectAllPaged.TotalRows,
            TotalPages: tabletypeSelectAllPaged.TotalPages,
            lstTableTypeModel: tabletypeSelectAllPaged.lstTableTypeModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    TableTypeModel.DeleteByTableTypeId = function (TableTypeId) {
        var URL = "/api/RamirezBar/TableType/1/DeleteByTableTypeId/" + TableTypeId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    TableTypeModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/TableType/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TableTypeModel.CopyByTableTypeId = function (TableTypeId) {
        var URL = "/api/RamirezBar/TableType/1/CopyByTableTypeId/" + TableTypeId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TableTypeModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/TableTypeing/TableType/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return TableTypeModel;
}());
exports.TableTypeModel = TableTypeModel;
//# sourceMappingURL=TableType_TsModel.js.map