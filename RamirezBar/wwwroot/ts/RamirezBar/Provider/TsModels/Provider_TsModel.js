"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProviderModel = void 0;
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
//10 fields | Sub-models: 1 models  | Last modification on: 20/08/2023 23:02:48 | Stack: 9
var ProviderModel = /** @class */ (function () {
    function ProviderModel() {
    }
    //Queries
    ProviderModel.Select1ByProviderId = function (ProviderId) {
        var URL = "/api/RamirezBar/Provider/1/Select1ByProviderIdToJSON/" + ProviderId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ProviderModel.SelectAll = function () {
        var URL = "/api/RamirezBar/Provider/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ProviderModel.SelectAllPaged = function (providerSelectAllPaged) {
        var URL = "/api/RamirezBar/Provider/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: providerSelectAllPaged.QueryString,
            ActualPageNumber: providerSelectAllPaged.ActualPageNumber,
            RowsPerPage: providerSelectAllPaged.RowsPerPage,
            SorterColumn: providerSelectAllPaged.SorterColumn,
            SortToggler: providerSelectAllPaged.SortToggler,
            RowCount: providerSelectAllPaged.TotalRows,
            TotalPages: providerSelectAllPaged.TotalPages,
            lstProviderModel: providerSelectAllPaged.lstProviderModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    //Non-Queries
    ProviderModel.DeleteByProviderId = function (ProviderId) {
        var URL = "/api/RamirezBar/Provider/1/DeleteByProviderId/" + ProviderId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ProviderModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/RamirezBar/Provider/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ProviderModel.CopyByProviderId = function (ProviderId) {
        var URL = "/api/RamirezBar/Provider/1/CopyByProviderId/" + ProviderId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ProviderModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Providering/Provider/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ProviderModel;
}());
exports.ProviderModel = ProviderModel;
//# sourceMappingURL=Provider_TsModel.js.map