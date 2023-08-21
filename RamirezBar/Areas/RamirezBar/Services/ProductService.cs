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

//Last modification on: 20/08/2023 23:17:57

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/08/2023 23:17:57
    /// </summary>
    public partial class ProductService : IProduct
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ProductService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ProductModel Select1ByProductIdToModel(int ProductId)
        {
            return new ProductModel().Select1ByProductIdToModel(ProductId);
        }

        public List<ProductModel> SelectAllToList()
        {
            return new ProductModel().SelectAllToList();
        }

        public productSelectAllPaged SelectAllPagedToModel(productSelectAllPaged productSelectAllPaged)
        {
            return new ProductModel().SelectAllPagedToModel(productSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ProductModel ProductModel)
        {
            return new ProductModel().Insert(ProductModel);
        }

        public int UpdateByProductId(ProductModel ProductModel)
        {
            return new ProductModel().UpdateByProductId(ProductModel);
        }

        public int DeleteByProductId(int ProductId)
        {
            return new ProductModel().DeleteByProductId(ProductId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ProductModel ProductModel = new ProductModel();
                ProductModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ProductModel ProductModel = new ProductModel().Select1ByProductIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ProductModel.DeleteByProductId(ProductModel.ProductId);
                }
            }
        }

        public int CopyByProductId(int ProductId)
        {
            ProductModel ProductModel = new ProductModel().Select1ByProductIdToModel(ProductId);
            int NewEnteredId = new ProductModel().Insert(ProductModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ProductModel> lstProductModel = new List<ProductModel> { };
                lstProductModel = new ProductModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstProductModel.Count];

                for (int i = 0; i < lstProductModel.Count; i++)
                {
                    NewEnteredIds[i] = lstProductModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ProductModel ProductModel = new ProductModel().Select1ByProductIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ProductModel.Insert();
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
            List<ProductModel> lstProductModel = new List<ProductModel> { };

            if (ExportationType == "All")
            {
                lstProductModel = new ProductModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ProductModel ProductModel = new ProductModel().Select1ByProductIdToModel(Convert.ToInt32(RowChecked));
                    lstProductModel.Add(ProductModel);
                }
            }

            foreach (ProductModel row in lstProductModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Product</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProductId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProviderId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Name&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Stock&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Photo&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/Product/Product_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtProduct = new DataTable();
                dtProduct.TableName = "Product";

                //We define another DataTable dtProductCopy to avoid issue related to DateTime conversion
                DataTable dtProductCopy = new DataTable();
                dtProductCopy.TableName = "Product";

                #region Define columns for dtProductCopy
                DataColumn dtColumnProductIdFordtProductCopy = new DataColumn();
                    dtColumnProductIdFordtProductCopy.DataType = typeof(string);
                    dtColumnProductIdFordtProductCopy.ColumnName = "ProductId";
                    dtProductCopy.Columns.Add(dtColumnProductIdFordtProductCopy);

                    DataColumn dtColumnActiveFordtProductCopy = new DataColumn();
                    dtColumnActiveFordtProductCopy.DataType = typeof(string);
                    dtColumnActiveFordtProductCopy.ColumnName = "Active";
                    dtProductCopy.Columns.Add(dtColumnActiveFordtProductCopy);

                    DataColumn dtColumnDateTimeCreationFordtProductCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtProductCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtProductCopy.ColumnName = "DateTimeCreation";
                    dtProductCopy.Columns.Add(dtColumnDateTimeCreationFordtProductCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtProductCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtProductCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtProductCopy.ColumnName = "DateTimeLastModification";
                    dtProductCopy.Columns.Add(dtColumnDateTimeLastModificationFordtProductCopy);

                    DataColumn dtColumnUserCreationIdFordtProductCopy = new DataColumn();
                    dtColumnUserCreationIdFordtProductCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtProductCopy.ColumnName = "UserCreationId";
                    dtProductCopy.Columns.Add(dtColumnUserCreationIdFordtProductCopy);

                    DataColumn dtColumnUserLastModificationIdFordtProductCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtProductCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtProductCopy.ColumnName = "UserLastModificationId";
                    dtProductCopy.Columns.Add(dtColumnUserLastModificationIdFordtProductCopy);

                    DataColumn dtColumnProviderIdFordtProductCopy = new DataColumn();
                    dtColumnProviderIdFordtProductCopy.DataType = typeof(string);
                    dtColumnProviderIdFordtProductCopy.ColumnName = "ProviderId";
                    dtProductCopy.Columns.Add(dtColumnProviderIdFordtProductCopy);

                    DataColumn dtColumnNameFordtProductCopy = new DataColumn();
                    dtColumnNameFordtProductCopy.DataType = typeof(string);
                    dtColumnNameFordtProductCopy.ColumnName = "Name";
                    dtProductCopy.Columns.Add(dtColumnNameFordtProductCopy);

                    DataColumn dtColumnStockFordtProductCopy = new DataColumn();
                    dtColumnStockFordtProductCopy.DataType = typeof(string);
                    dtColumnStockFordtProductCopy.ColumnName = "Stock";
                    dtProductCopy.Columns.Add(dtColumnStockFordtProductCopy);

                    DataColumn dtColumnPhotoFordtProductCopy = new DataColumn();
                    dtColumnPhotoFordtProductCopy.DataType = typeof(string);
                    dtColumnPhotoFordtProductCopy.ColumnName = "Photo";
                    dtProductCopy.Columns.Add(dtColumnPhotoFordtProductCopy);

                    
                #endregion

                dtProduct = new ProductModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtProduct.Rows)
                {
                    dtProductCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtProductCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Producting/Product/Product_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsProduct = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtProductCopy to avoid issue related to DateTime conversion
                    DataTable dtProductCopy = new DataTable();
                    dtProductCopy.TableName = "Product";

                    #region Define columns for dtProductCopy
                    DataColumn dtColumnProductIdFordtProductCopy = new DataColumn();
                    dtColumnProductIdFordtProductCopy.DataType = typeof(string);
                    dtColumnProductIdFordtProductCopy.ColumnName = "ProductId";
                    dtProductCopy.Columns.Add(dtColumnProductIdFordtProductCopy);

                    DataColumn dtColumnActiveFordtProductCopy = new DataColumn();
                    dtColumnActiveFordtProductCopy.DataType = typeof(string);
                    dtColumnActiveFordtProductCopy.ColumnName = "Active";
                    dtProductCopy.Columns.Add(dtColumnActiveFordtProductCopy);

                    DataColumn dtColumnDateTimeCreationFordtProductCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtProductCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtProductCopy.ColumnName = "DateTimeCreation";
                    dtProductCopy.Columns.Add(dtColumnDateTimeCreationFordtProductCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtProductCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtProductCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtProductCopy.ColumnName = "DateTimeLastModification";
                    dtProductCopy.Columns.Add(dtColumnDateTimeLastModificationFordtProductCopy);

                    DataColumn dtColumnUserCreationIdFordtProductCopy = new DataColumn();
                    dtColumnUserCreationIdFordtProductCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtProductCopy.ColumnName = "UserCreationId";
                    dtProductCopy.Columns.Add(dtColumnUserCreationIdFordtProductCopy);

                    DataColumn dtColumnUserLastModificationIdFordtProductCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtProductCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtProductCopy.ColumnName = "UserLastModificationId";
                    dtProductCopy.Columns.Add(dtColumnUserLastModificationIdFordtProductCopy);

                    DataColumn dtColumnProviderIdFordtProductCopy = new DataColumn();
                    dtColumnProviderIdFordtProductCopy.DataType = typeof(string);
                    dtColumnProviderIdFordtProductCopy.ColumnName = "ProviderId";
                    dtProductCopy.Columns.Add(dtColumnProviderIdFordtProductCopy);

                    DataColumn dtColumnNameFordtProductCopy = new DataColumn();
                    dtColumnNameFordtProductCopy.DataType = typeof(string);
                    dtColumnNameFordtProductCopy.ColumnName = "Name";
                    dtProductCopy.Columns.Add(dtColumnNameFordtProductCopy);

                    DataColumn dtColumnStockFordtProductCopy = new DataColumn();
                    dtColumnStockFordtProductCopy.DataType = typeof(string);
                    dtColumnStockFordtProductCopy.ColumnName = "Stock";
                    dtProductCopy.Columns.Add(dtColumnStockFordtProductCopy);

                    DataColumn dtColumnPhotoFordtProductCopy = new DataColumn();
                    dtColumnPhotoFordtProductCopy.DataType = typeof(string);
                    dtColumnPhotoFordtProductCopy.ColumnName = "Photo";
                    dtProductCopy.Columns.Add(dtColumnPhotoFordtProductCopy);

                    
                    #endregion

                    dsProduct.Tables.Add(dtProductCopy);

                    for (int i = 0; i < dsProduct.Tables.Count; i++)
                    {
                        dtProductCopy = new ProductModel().Select1ByProductIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtProductCopy.Rows)
                        {
                            dsProduct.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsProduct.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsProduct.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Producting/Product/Product_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ProductModel> lstProductModel = new List<ProductModel> { };

            if (ExportationType == "All")
            {
                lstProductModel = new ProductModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ProductModel ProductModel = new ProductModel().Select1ByProductIdToModel(Convert.ToInt32(RowChecked));
                    lstProductModel.Add(ProductModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Producting/Product/Product_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstProductModel);
            }

            return Now;
        }
        #endregion
    }
}