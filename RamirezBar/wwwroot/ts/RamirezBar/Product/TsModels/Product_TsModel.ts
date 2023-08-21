import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { productSelectAllPaged } from "../DTOs/productSelectAllPaged";


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

//11 fields | Sub-models: 0 models  | Last modification on: 21/08/2023 6:49:20 | Stack: 9

export class ProductModel {

    //Fields
    ProductId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ProviderId?: number;
	Name?: string | string[] | number | undefined;
	Stock?: number;
	Photo?: string | string[] | number | undefined;
	Price?: number;
    ProviderName?: string | string[] | number | undefined;

    //Queries
    static Select1ByProductId(ProductId: number) {
        let URL = "/api/RamirezBar/Product/1/Select1ByProductIdToJSON/" + ProductId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/Product/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(productSelectAllPaged: productSelectAllPaged) {
        let URL = "/api/RamirezBar/Product/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: productSelectAllPaged.QueryString,
            ActualPageNumber: productSelectAllPaged.ActualPageNumber,
            RowsPerPage: productSelectAllPaged.RowsPerPage,
            SorterColumn: productSelectAllPaged.SorterColumn,
            SortToggler: productSelectAllPaged.SortToggler,
            RowCount: productSelectAllPaged.TotalRows,
            TotalPages: productSelectAllPaged.TotalPages,
            lstProductModel: productSelectAllPaged.lstProductModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByProductId(ProductId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/Product/1/DeleteByProductId/" + ProductId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/Product/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByProductId(ProductId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/Product/1/CopyByProductId/" + ProductId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Producting/Product/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}