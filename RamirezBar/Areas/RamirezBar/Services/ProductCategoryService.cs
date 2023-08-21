using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using RamirezBar.Areas.RamirezBar.Models;
using RamirezBar.Areas.RamirezBar.DTOs;
using RamirezBar.Areas.RamirezBar.Interfaces;
using RamirezBar.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

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

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/08/2023 23:02:42
    /// </summary>
    public partial class ProductCategoryService : IProductCategory
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ProductCategoryService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ProductCategoryModel Select1ByProductCategoryIdToModel(int ProductCategoryId)
        {
            return new ProductCategoryModel().Select1ByProductCategoryIdToModel(ProductCategoryId);
        }

        public List<ProductCategoryModel> SelectAllToList()
        {
            return new ProductCategoryModel().SelectAllToList();
        }

        public productcategorySelectAllPaged SelectAllPagedToModel(productcategorySelectAllPaged productcategorySelectAllPaged)
        {
            return new ProductCategoryModel().SelectAllPagedToModel(productcategorySelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ProductCategoryModel ProductCategoryModel)
        {
            return new ProductCategoryModel().Insert(ProductCategoryModel);
        }

        public int UpdateByProductCategoryId(ProductCategoryModel ProductCategoryModel)
        {
            return new ProductCategoryModel().UpdateByProductCategoryId(ProductCategoryModel);
        }

        public int DeleteByProductCategoryId(int ProductCategoryId)
        {
            return new ProductCategoryModel().DeleteByProductCategoryId(ProductCategoryId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ProductCategoryModel ProductCategoryModel = new ProductCategoryModel();
                ProductCategoryModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ProductCategoryModel ProductCategoryModel = new ProductCategoryModel().Select1ByProductCategoryIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ProductCategoryModel.DeleteByProductCategoryId(ProductCategoryModel.ProductCategoryId);
                }
            }
        }

        public int CopyByProductCategoryId(int ProductCategoryId)
        {
            ProductCategoryModel ProductCategoryModel = new ProductCategoryModel().Select1ByProductCategoryIdToModel(ProductCategoryId);
            int NewEnteredId = new ProductCategoryModel().Insert(ProductCategoryModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ProductCategoryModel> lstProductCategoryModel = new List<ProductCategoryModel> { };
                lstProductCategoryModel = new ProductCategoryModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstProductCategoryModel.Count];

                for (int i = 0; i < lstProductCategoryModel.Count; i++)
                {
                    NewEnteredIds[i] = lstProductCategoryModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ProductCategoryModel ProductCategoryModel = new ProductCategoryModel().Select1ByProductCategoryIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ProductCategoryModel.Insert();
                }

                return NewEnteredIds;
            }
        }
        #endregion

        #region Other services
        public DateTime ExportAsPDF(Ajax Ajax, string ExportationType)
        {
            var Renderer = new HtmlToPdf();
            DateTime Now = DateTime.Now;
            string RowsAsHTML = "";
            List<ProductCategoryModel> lstProductCategoryModel = new List<ProductCategoryModel> { };

            if (ExportationType == "All")
            {
                lstProductCategoryModel = new ProductCategoryModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ProductCategoryModel ProductCategoryModel = new ProductCategoryModel().Select1ByProductCategoryIdToModel(Convert.ToInt32(RowChecked));
                    lstProductCategoryModel.Add(ProductCategoryModel);
                }
            }

            foreach (ProductCategoryModel row in lstProductCategoryModel)
            {
                RowsAsHTML += $@"{row.ToStringOnlyValuesForHTML()}";
            }

            Renderer.RenderHtmlAsPdf($@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""88%"" style=""width: 88% !important; min-width: 88%; max-width: 88%;"">
    <tr>
    <td align=""left"" valign=""top"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#1a1a1a"" style=""font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #1a1a1a; font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">Mikromatica</span>
        </font>
        <div style=""height: 25px; line-height: 25px; font-size: 23px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#4c4c4c"" style=""font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of ProductCategory</span>
        </font>
        <div style=""height: 35px; line-height: 35px; font-size: 33px;"">&nbsp;</div>
    </td>
    </tr>
</table>
<br>
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""width: 100% !important; min-width: 100%; max-width: 100%;"">
    <tr>
        <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProductCategoryId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserCreationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserLastModificationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Name&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th>
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {Now}</span>
</font>
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/ProductCategory/ProductCategory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtProductCategory = new DataTable();
                dtProductCategory.TableName = "ProductCategory";

                //We define another DataTable dtProductCategoryCopy to avoid issue related to DateTime conversion
                DataTable dtProductCategoryCopy = new DataTable();
                dtProductCategoryCopy.TableName = "ProductCategory";

                #region Define columns for dtProductCategoryCopy
                DataColumn dtColumnProductCategoryIdFordtProductCategoryCopy = new DataColumn();
                    dtColumnProductCategoryIdFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnProductCategoryIdFordtProductCategoryCopy.ColumnName = "ProductCategoryId";
                    dtProductCategoryCopy.Columns.Add(dtColumnProductCategoryIdFordtProductCategoryCopy);

                    DataColumn dtColumnActiveFordtProductCategoryCopy = new DataColumn();
                    dtColumnActiveFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnActiveFordtProductCategoryCopy.ColumnName = "Active";
                    dtProductCategoryCopy.Columns.Add(dtColumnActiveFordtProductCategoryCopy);

                    DataColumn dtColumnDateTimeCreationFordtProductCategoryCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtProductCategoryCopy.ColumnName = "DateTimeCreation";
                    dtProductCategoryCopy.Columns.Add(dtColumnDateTimeCreationFordtProductCategoryCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtProductCategoryCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtProductCategoryCopy.ColumnName = "DateTimeLastModification";
                    dtProductCategoryCopy.Columns.Add(dtColumnDateTimeLastModificationFordtProductCategoryCopy);

                    DataColumn dtColumnUserCreationIdFordtProductCategoryCopy = new DataColumn();
                    dtColumnUserCreationIdFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtProductCategoryCopy.ColumnName = "UserCreationId";
                    dtProductCategoryCopy.Columns.Add(dtColumnUserCreationIdFordtProductCategoryCopy);

                    DataColumn dtColumnUserLastModificationIdFordtProductCategoryCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtProductCategoryCopy.ColumnName = "UserLastModificationId";
                    dtProductCategoryCopy.Columns.Add(dtColumnUserLastModificationIdFordtProductCategoryCopy);

                    DataColumn dtColumnNameFordtProductCategoryCopy = new DataColumn();
                    dtColumnNameFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnNameFordtProductCategoryCopy.ColumnName = "Name";
                    dtProductCategoryCopy.Columns.Add(dtColumnNameFordtProductCategoryCopy);

                    
                #endregion

                dtProductCategory = new ProductCategoryModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtProductCategory.Rows)
                {
                    dtProductCategoryCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtProductCategoryCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/ProductCategorying/ProductCategory/ProductCategory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsProductCategory = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtProductCategoryCopy to avoid issue related to DateTime conversion
                    DataTable dtProductCategoryCopy = new DataTable();
                    dtProductCategoryCopy.TableName = "ProductCategory";

                    #region Define columns for dtProductCategoryCopy
                    DataColumn dtColumnProductCategoryIdFordtProductCategoryCopy = new DataColumn();
                    dtColumnProductCategoryIdFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnProductCategoryIdFordtProductCategoryCopy.ColumnName = "ProductCategoryId";
                    dtProductCategoryCopy.Columns.Add(dtColumnProductCategoryIdFordtProductCategoryCopy);

                    DataColumn dtColumnActiveFordtProductCategoryCopy = new DataColumn();
                    dtColumnActiveFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnActiveFordtProductCategoryCopy.ColumnName = "Active";
                    dtProductCategoryCopy.Columns.Add(dtColumnActiveFordtProductCategoryCopy);

                    DataColumn dtColumnDateTimeCreationFordtProductCategoryCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtProductCategoryCopy.ColumnName = "DateTimeCreation";
                    dtProductCategoryCopy.Columns.Add(dtColumnDateTimeCreationFordtProductCategoryCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtProductCategoryCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtProductCategoryCopy.ColumnName = "DateTimeLastModification";
                    dtProductCategoryCopy.Columns.Add(dtColumnDateTimeLastModificationFordtProductCategoryCopy);

                    DataColumn dtColumnUserCreationIdFordtProductCategoryCopy = new DataColumn();
                    dtColumnUserCreationIdFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtProductCategoryCopy.ColumnName = "UserCreationId";
                    dtProductCategoryCopy.Columns.Add(dtColumnUserCreationIdFordtProductCategoryCopy);

                    DataColumn dtColumnUserLastModificationIdFordtProductCategoryCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtProductCategoryCopy.ColumnName = "UserLastModificationId";
                    dtProductCategoryCopy.Columns.Add(dtColumnUserLastModificationIdFordtProductCategoryCopy);

                    DataColumn dtColumnNameFordtProductCategoryCopy = new DataColumn();
                    dtColumnNameFordtProductCategoryCopy.DataType = typeof(string);
                    dtColumnNameFordtProductCategoryCopy.ColumnName = "Name";
                    dtProductCategoryCopy.Columns.Add(dtColumnNameFordtProductCategoryCopy);

                    
                    #endregion

                    dsProductCategory.Tables.Add(dtProductCategoryCopy);

                    for (int i = 0; i < dsProductCategory.Tables.Count; i++)
                    {
                        dtProductCategoryCopy = new ProductCategoryModel().Select1ByProductCategoryIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtProductCategoryCopy.Rows)
                        {
                            dsProductCategory.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsProductCategory.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsProductCategory.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/ProductCategorying/ProductCategory/ProductCategory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ProductCategoryModel> lstProductCategoryModel = new List<ProductCategoryModel> { };

            if (ExportationType == "All")
            {
                lstProductCategoryModel = new ProductCategoryModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ProductCategoryModel ProductCategoryModel = new ProductCategoryModel().Select1ByProductCategoryIdToModel(Convert.ToInt32(RowChecked));
                    lstProductCategoryModel.Add(ProductCategoryModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/ProductCategorying/ProductCategory/ProductCategory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstProductCategoryModel);
            }

            return Now;
        }
        #endregion
    }
}