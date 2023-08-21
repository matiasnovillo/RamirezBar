import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { clienteSelectAllPaged } from "../DTOs/clienteSelectAllPaged";


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

//7 fields | Sub-models: 0 models  | Last modification on: 20/08/2023 22:48:35 | Stack: 9

export class ClienteModel {

    //Fields
    ClienteId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
    

    //Queries
    static Select1ByClienteId(ClienteId: number) {
        let URL = "/api/RamirezBar/Cliente/1/Select1ByClienteIdToJSON/" + ClienteId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/Cliente/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(clienteSelectAllPaged: clienteSelectAllPaged) {
        let URL = "/api/RamirezBar/Cliente/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: clienteSelectAllPaged.QueryString,
            ActualPageNumber: clienteSelectAllPaged.ActualPageNumber,
            RowsPerPage: clienteSelectAllPaged.RowsPerPage,
            SorterColumn: clienteSelectAllPaged.SorterColumn,
            SortToggler: clienteSelectAllPaged.SortToggler,
            RowCount: clienteSelectAllPaged.TotalRows,
            TotalPages: clienteSelectAllPaged.TotalPages,
            lstClienteModel: clienteSelectAllPaged.lstClienteModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByClienteId(ClienteId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/Cliente/1/DeleteByClienteId/" + ClienteId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/Cliente/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByClienteId(ClienteId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/Cliente/1/CopyByClienteId/" + ClienteId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Clienteing/Cliente/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}