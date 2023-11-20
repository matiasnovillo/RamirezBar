import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { pointSelectAllPaged } from "../DTOs/pointSelectAllPaged";


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

export class PointModel {

    //Fields
    PointId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	DNI?: number;
	Point?: number;
    

    //Queries
    static Select1ByPointId(PointId: number) {
        let URL = "/api/RamirezBar/Point/1/Select1ByPointIdToJSON/" + PointId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/Point/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(pointSelectAllPaged: pointSelectAllPaged) {
        let URL = "/api/RamirezBar/Point/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: pointSelectAllPaged.QueryString,
            ActualPageNumber: pointSelectAllPaged.ActualPageNumber,
            RowsPerPage: pointSelectAllPaged.RowsPerPage,
            SorterColumn: pointSelectAllPaged.SorterColumn,
            SortToggler: pointSelectAllPaged.SortToggler,
            RowCount: pointSelectAllPaged.TotalRows,
            TotalPages: pointSelectAllPaged.TotalPages,
            lstPointModel: pointSelectAllPaged.lstPointModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByPointId(PointId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/Point/1/DeleteByPointId/" + PointId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/Point/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByPointId(PointId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/Point/1/CopyByPointId/" + PointId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Pointing/Point/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}