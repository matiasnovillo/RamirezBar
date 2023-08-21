"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProductModel = void 0;
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
//10 fields | Sub-models: 0 models  | Last modification on: 20/08/2023 23:17:57 | Stack: 9
var ProductModel = /** @class */ (function () {
    function ProductModel() {
    }
    //Queries
    ProductModel.Select1ByProductId = function (ProductId) {
        var URL = "/api/RamirezBar/Product/1/Select1ByProductIdToJSON/" + ProductId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ProductModel.SelectAll = function () {
        var URL = "/api/RamirezBar/Product/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ProductModel.SelectAllPaged = function (productSelectAllPaged) {
        var URL = "/api/RamirezBar/Product/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: productSelectAllPaged.QueryString,
            ActualPageNumber: productSelectAllPaged.ActualPageNumber,
            RowsPerPage: productSelectAllPaged.RowsPerPage,
            SorterColumn: productSelectAllPaged.SorterColumn,
            SortToggler: productSelectAllPaged.SortToggler,
            RowCount: productSelectAllPaged.TotalRows,
            TotalPages: productSelectAllPaged.TotalPages,
            lstProductModel: productSelectAllPaged.lstProductModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    ProductModel.DeleteByProductId = function (ProductId) {
        var URL = "/api/RamirezBar/Product/1/DeleteByProductId/" + ProductId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ProductModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/Product/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ProductModel.CopyByProductId = function (ProductId) {
        var URL = "/api/RamirezBar/Product/1/CopyByProductId/" + ProductId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ProductModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Producting/Product/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ProductModel;
}());
exports.ProductModel = ProductModel;
//# sourceMappingURL=Product_TsModel.js.map