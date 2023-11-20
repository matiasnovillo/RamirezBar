"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PointModel = void 0;
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
//8 fields | Sub-models: 0 models  | Last modification on: 06/11/2023 14:38:45 | Stack: 9
var PointModel = /** @class */ (function () {
    function PointModel() {
    }
    //Queries
    PointModel.Select1ByPointId = function (PointId) {
        var URL = "/api/RamirezBar/Point/1/Select1ByPointIdToJSON/" + PointId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    PointModel.SelectAll = function () {
        var URL = "/api/RamirezBar/Point/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    PointModel.SelectAllPaged = function (pointSelectAllPaged) {
        var URL = "/api/RamirezBar/Point/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: pointSelectAllPaged.QueryString,
            ActualPageNumber: pointSelectAllPaged.ActualPageNumber,
            RowsPerPage: pointSelectAllPaged.RowsPerPage,
            SorterColumn: pointSelectAllPaged.SorterColumn,
            SortToggler: pointSelectAllPaged.SortToggler,
            RowCount: pointSelectAllPaged.TotalRows,
            TotalPages: pointSelectAllPaged.TotalPages,
            lstPointModel: pointSelectAllPaged.lstPointModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    PointModel.DeleteByPointId = function (PointId) {
        var URL = "/api/RamirezBar/Point/1/DeleteByPointId/" + PointId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    PointModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/Point/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    PointModel.CopyByPointId = function (PointId) {
        var URL = "/api/RamirezBar/Point/1/CopyByPointId/" + PointId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    PointModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Pointing/Point/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return PointModel;
}());
exports.PointModel = PointModel;
//# sourceMappingURL=Point_TsModel.js.map