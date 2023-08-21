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

//Last modification on: 20/08/2023 23:02:42

namespace RamirezBar.Areas.RamirezBar.Interfaces
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Interface. <br/>
    /// Function:          This interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 20/08/2023 23:02:42
    /// </summary>
    public partial interface IProductCategory
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="ProductCategoryId"></param>
        /// <returns></returns>
        ProductCategoryModel Select1ByProductCategoryIdToModel(int ProductCategoryId);

        List<ProductCategoryModel> SelectAllToList();

        productcategorySelectAllPaged SelectAllPagedToModel(productcategorySelectAllPaged productcategorySelectAllPaged);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="ProductCategory"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in ProductCategory table</returns>
        int Insert(ProductCategoryModel ProductCategory);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="ProductCategory"></param>
        /// <returns>The number of rows updated in ProductCategory table</returns>
        int UpdateByProductCategoryId(ProductCategoryModel ProductCategory);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="ProductCategoryId"></param>
        /// <returns>The number of rows deleted in ProductCategory table</returns>
        int DeleteByProductCategoryId(int ProductCategoryId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByProductCategoryId(int ProductCategoryId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}