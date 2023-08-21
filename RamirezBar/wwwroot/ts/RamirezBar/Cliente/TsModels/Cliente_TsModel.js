"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ClienteModel = void 0;
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
//7 fields | Sub-models: 0 models  | Last modification on: 20/08/2023 22:48:35 | Stack: 9
var ClienteModel = /** @class */ (function () {
    function ClienteModel() {
    }
    //Queries
    ClienteModel.Select1ByClienteId = function (ClienteId) {
        var URL = "/api/RamirezBar/Cliente/1/Select1ByClienteIdToJSON/" + ClienteId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ClienteModel.SelectAll = function () {
        var URL = "/api/RamirezBar/Cliente/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ClienteModel.SelectAllPaged = function (clienteSelectAllPaged) {
        var URL = "/api/RamirezBar/Cliente/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: clienteSelectAllPaged.QueryString,
            ActualPageNumber: clienteSelectAllPaged.ActualPageNumber,
            RowsPerPage: clienteSelectAllPaged.RowsPerPage,
            SorterColumn: clienteSelectAllPaged.SorterColumn,
            SortToggler: clienteSelectAllPaged.SortToggler,
            RowCount: clienteSelectAllPaged.TotalRows,
            TotalPages: clienteSelectAllPaged.TotalPages,
            lstClienteModel: clienteSelectAllPaged.lstClienteModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    ClienteModel.DeleteByClienteId = function (ClienteId) {
        var URL = "/api/RamirezBar/Cliente/1/DeleteByClienteId/" + ClienteId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ClienteModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/Cliente/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ClienteModel.CopyByClienteId = function (ClienteId) {
        var URL = "/api/RamirezBar/Cliente/1/CopyByClienteId/" + ClienteId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ClienteModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Clienteing/Cliente/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ClienteModel;
}());
exports.ClienteModel = ClienteModel;
//# sourceMappingURL=Cliente_TsModel.js.map