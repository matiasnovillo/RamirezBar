using RamirezBar.Areas.RamirezBar.DTOs;
using RamirezBar.Areas.RamirezBar.Models;
using RamirezBar.Library;
using System;
using System.Collections.Generic;

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

//Last modification on: 21/08/2023 6:56:48

namespace RamirezBar.Areas.RamirezBar.Interfaces
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Interface. <br/>
    /// Function:          This interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 21/08/2023 6:56:48
    /// </summary>
    public partial interface ITable
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="TableId"></param>
        /// <returns></returns>
        TableModel Select1ByTableIdToModel(int TableId);

        List<TableModel> SelectAllToList();

        tableSelectAllPaged SelectAllPagedToModel(tableSelectAllPaged tableSelectAllPaged);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="Table"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Table table</returns>
        int Insert(TableModel Table);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="Table"></param>
        /// <returns>The number of rows updated in Table table</returns>
        int UpdateByTableId(TableModel Table);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="TableId"></param>
        /// <returns>The number of rows deleted in Table table</returns>
        int DeleteByTableId(int TableId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByTableId(int TableId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}