import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { tableSelectAllPaged } from "../DTOs/tableSelectAllPaged";


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

//10 fields | Sub-models: 0 models  | Last modification on: 20/08/2023 23:27:14 | Stack: 9

export class TableModel {

    //Fields
    TableId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
	Photo?: string | string[] | number | undefined;
	UserWaiterId?: number;
	TableStateId?: number;
    

    //Queries
    static Select1ByTableId(TableId: number) {
        let URL = "/api/RamirezBar/Table/1/Select1ByTableIdToJSON/" + TableId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/Table/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(tableSelectAllPaged: tableSelectAllPaged) {
        let URL = "/api/RamirezBar/Table/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: tableSelectAllPaged.QueryString,
            ActualPageNumber: tableSelectAllPaged.ActualPageNumber,
            RowsPerPage: tableSelectAllPaged.RowsPerPage,
            SorterColumn: tableSelectAllPaged.SorterColumn,
            SortToggler: tableSelectAllPaged.SortToggler,
            RowCount: tableSelectAllPaged.TotalRows,
            TotalPages: tableSelectAllPaged.TotalPages,
            lstTableModel: tableSelectAllPaged.lstTableModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByTableId(TableId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/Table/1/DeleteByTableId/" + TableId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/Table/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByTableId(TableId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/Table/1/CopyByTableId/" + TableId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Tableing/Table/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}