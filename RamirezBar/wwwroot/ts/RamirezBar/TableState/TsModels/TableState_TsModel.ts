import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { tablestateSelectAllPaged } from "../DTOs/tablestateSelectAllPaged";
import { TableModel } from "../../Table/TsModels/Table_TsModel";

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

//7 fields | Sub-models: 1 models  | Last modification on: 21/08/2023 6:20:18 | Stack: 9

export class TableStateModel {

    //Fields
    TableStateId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
    lstTableModel?: TableModel[] | undefined;
    

    //Queries
    static Select1ByTableStateId(TableStateId: number) {
        let URL = "/api/RamirezBar/TableState/1/Select1ByTableStateIdToJSON/" + TableStateId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/TableState/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(tablestateSelectAllPaged: tablestateSelectAllPaged) {
        let URL = "/api/RamirezBar/TableState/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: tablestateSelectAllPaged.QueryString,
            ActualPageNumber: tablestateSelectAllPaged.ActualPageNumber,
            RowsPerPage: tablestateSelectAllPaged.RowsPerPage,
            SorterColumn: tablestateSelectAllPaged.SorterColumn,
            SortToggler: tablestateSelectAllPaged.SortToggler,
            RowCount: tablestateSelectAllPaged.TotalRows,
            TotalPages: tablestateSelectAllPaged.TotalPages,
            lstTableStateModel: tablestateSelectAllPaged.lstTableStateModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByTableStateId(TableStateId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/TableState/1/DeleteByTableStateId/" + TableStateId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/TableState/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByTableStateId(TableStateId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/TableState/1/CopyByTableStateId/" + TableStateId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/TableStateing/TableState/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}