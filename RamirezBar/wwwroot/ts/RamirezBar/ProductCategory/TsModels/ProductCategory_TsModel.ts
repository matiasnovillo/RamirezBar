import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { productcategorySelectAllPaged } from "../DTOs/productcategorySelectAllPaged";


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

export class ProductCategoryModel {

    //Fields
    ProductCategoryId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
    

    //Queries
    static Select1ByProductCategoryId(ProductCategoryId: number) {
        let URL = "/api/RamirezBar/ProductCategory/1/Select1ByProductCategoryIdToJSON/" + ProductCategoryId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/ProductCategory/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(productcategorySelectAllPaged: productcategorySelectAllPaged) {
        let URL = "/api/RamirezBar/ProductCategory/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: productcategorySelectAllPaged.QueryString,
            ActualPageNumber: productcategorySelectAllPaged.ActualPageNumber,
            RowsPerPage: productcategorySelectAllPaged.RowsPerPage,
            SorterColumn: productcategorySelectAllPaged.SorterColumn,
            SortToggler: productcategorySelectAllPaged.SortToggler,
            RowCount: productcategorySelectAllPaged.TotalRows,
            TotalPages: productcategorySelectAllPaged.TotalPages,
            lstProductCategoryModel: productcategorySelectAllPaged.lstProductCategoryModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByProductCategoryId(ProductCategoryId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/ProductCategory/1/DeleteByProductCategoryId/" + ProductCategoryId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/ProductCategory/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByProductCategoryId(ProductCategoryId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/ProductCategory/1/CopyByProductCategoryId/" + ProductCategoryId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/ProductCategorying/ProductCategory/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}