import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { tabletypeSelectAllPaged } from "../DTOs/tabletypeSelectAllPaged";


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

export class TableTypeModel {

    //Fields
    TableTypeId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
    

    //Queries
    static Select1ByTableTypeId(TableTypeId: number) {
        let URL = "/api/RamirezBar/TableType/1/Select1ByTableTypeIdToJSON/" + TableTypeId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/TableType/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(tabletypeSelectAllPaged: tabletypeSelectAllPaged) {
        let URL = "/api/RamirezBar/TableType/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: tabletypeSelectAllPaged.QueryString,
            ActualPageNumber: tabletypeSelectAllPaged.ActualPageNumber,
            RowsPerPage: tabletypeSelectAllPaged.RowsPerPage,
            SorterColumn: tabletypeSelectAllPaged.SorterColumn,
            SortToggler: tabletypeSelectAllPaged.SortToggler,
            RowCount: tabletypeSelectAllPaged.TotalRows,
            TotalPages: tabletypeSelectAllPaged.TotalPages,
            lstTableTypeModel: tabletypeSelectAllPaged.lstTableTypeModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByTableTypeId(TableTypeId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/TableType/1/DeleteByTableTypeId/" + TableTypeId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/TableType/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByTableTypeId(TableTypeId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/TableType/1/CopyByTableTypeId/" + TableTypeId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/TableTypeing/TableType/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}