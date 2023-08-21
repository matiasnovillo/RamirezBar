"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProductCategoryModel = void 0;
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
//7 fields | Sub-models: 0 models  | Last modification on: 20/08/2023 23:02:42 | Stack: 9
var ProductCategoryModel = /** @class */ (function () {
    function ProductCategoryModel() {
    }
    //Queries
    ProductCategoryModel.Select1ByProductCategoryId = function (ProductCategoryId) {
        var URL = "/api/RamirezBar/ProductCategory/1/Select1ByProductCategoryIdToJSON/" + ProductCategoryId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ProductCategoryModel.SelectAll = function () {
        var URL = "/api/RamirezBar/ProductCategory/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ProductCategoryModel.SelectAllPaged = function (productcategorySelectAllPaged) {
        var URL = "/api/RamirezBar/ProductCategory/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: productcategorySelectAllPaged.QueryString,
            ActualPageNumber: productcategorySelectAllPaged.ActualPageNumber,
            RowsPerPage: productcategorySelectAllPaged.RowsPerPage,
            SorterColumn: productcategorySelectAllPaged.SorterColumn,
            SortToggler: productcategorySelectAllPaged.SortToggler,
            RowCount: productcategorySelectAllPaged.TotalRows,
            TotalPages: productcategorySelectAllPaged.TotalPages,
            lstProductCategoryModel: productcategorySelectAllPaged.lstProductCategoryModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    ProductCategoryModel.DeleteByProductCategoryId = function (ProductCategoryId) {
        var URL = "/api/RamirezBar/ProductCategory/1/DeleteByProductCategoryId/" + ProductCategoryId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ProductCategoryModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/ProductCategory/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ProductCategoryModel.CopyByProductCategoryId = function (ProductCategoryId) {
        var URL = "/api/RamirezBar/ProductCategory/1/CopyByProductCategoryId/" + ProductCategoryId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ProductCategoryModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/ProductCategorying/ProductCategory/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ProductCategoryModel;
}());
exports.ProductCategoryModel = ProductCategoryModel;
//# sourceMappingURL=ProductCategory_TsModel.js.map