import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { providerSelectAllPaged } from "../DTOs/providerSelectAllPaged";
import { ProductModel } from "../../Product/TsModels/Product_TsModel";

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

export class ProviderModel {

    //Fields
    ProviderId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
	Address?: string | string[] | number | undefined;
	Phone1?: string | string[] | number | undefined;
	Phone2?: string | string[] | number | undefined;
    lstProductModel?: ProductModel[] | undefined;
    

    //Queries
    static Select1ByProviderId(ProviderId: number) {
        let URL = "/api/RamirezBar/Provider/1/Select1ByProviderIdToJSON/" + ProviderId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/RamirezBar/Provider/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(providerSelectAllPaged: providerSelectAllPaged) {
        let URL = "/api/RamirezBar/Provider/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: providerSelectAllPaged.QueryString,
            ActualPageNumber: providerSelectAllPaged.ActualPageNumber,
            RowsPerPage: providerSelectAllPaged.RowsPerPage,
            SorterColumn: providerSelectAllPaged.SorterColumn,
            SortToggler: providerSelectAllPaged.SortToggler,
            RowCount: providerSelectAllPaged.TotalRows,
            TotalPages: providerSelectAllPaged.TotalPages,
            lstProviderModel: providerSelectAllPaged.lstProviderModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByProviderId(ProviderId: number | string | string[] | undefined) {
        let URL = "/api/RamirezBar/Provider/1/DeleteByProviderId/" + ProviderId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/RamirezBar/Provider/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByProviderId(ProviderId: string | number | string[] | undefined) {
        let URL = "/api/RamirezBar/Provider/1/CopyByProviderId/" + ProviderId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Providering/Provider/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}