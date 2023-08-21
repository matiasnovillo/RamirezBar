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

//Last modification on: 20/08/2023 23:03:00

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/08/2023 23:03:00
    /// </summary>
    public partial class TableTypeService : ITableType
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public TableTypeService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public TableTypeModel Select1ByTableTypeIdToModel(int TableTypeId)
        {
            return new TableTypeModel().Select1ByTableTypeIdToModel(TableTypeId);
        }

        public List<TableTypeModel> SelectAllToList()
        {
            return new TableTypeModel().SelectAllToList();
        }

        public tabletypeSelectAllPaged SelectAllPagedToModel(tabletypeSelectAllPaged tabletypeSelectAllPaged)
        {
            return new TableTypeModel().SelectAllPagedToModel(tabletypeSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(TableTypeModel TableTypeModel)
        {
            return new TableTypeModel().Insert(TableTypeModel);
        }

        public int UpdateByTableTypeId(TableTypeModel TableTypeModel)
        {
            return new TableTypeModel().UpdateByTableTypeId(TableTypeModel);
        }

        public int DeleteByTableTypeId(int TableTypeId)
        {
            return new TableTypeModel().DeleteByTableTypeId(TableTypeId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                TableTypeModel TableTypeModel = new TableTypeModel();
                TableTypeModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TableTypeModel TableTypeModel = new TableTypeModel().Select1ByTableTypeIdToModel(Convert.ToInt32(RowsChecked[i]));
                    TableTypeModel.DeleteByTableTypeId(TableTypeModel.TableTypeId);
                }
            }
        }

        public int CopyByTableTypeId(int TableTypeId)
        {
            TableTypeModel TableTypeModel = new TableTypeModel().Select1ByTableTypeIdToModel(TableTypeId);
            int NewEnteredId = new TableTypeModel().Insert(TableTypeModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<TableTypeModel> lstTableTypeModel = new List<TableTypeModel> { };
                lstTableTypeModel = new TableTypeModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstTableTypeModel.Count];

                for (int i = 0; i < lstTableTypeModel.Count; i++)
                {
                    NewEnteredIds[i] = lstTableTypeModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TableTypeModel TableTypeModel = new TableTypeModel().Select1ByTableTypeIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = TableTypeModel.Insert();
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
            List<TableTypeModel> lstTableTypeModel = new List<TableTypeModel> { };

            if (ExportationType == "All")
            {
                lstTableTypeModel = new TableTypeModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TableTypeModel TableTypeModel = new TableTypeModel().Select1ByTableTypeIdToModel(Convert.ToInt32(RowChecked));
                    lstTableTypeModel.Add(TableTypeModel);
                }
            }

            foreach (TableTypeModel row in lstTableTypeModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of TableType</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TableTypeId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/TableType/TableType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtTableType = new DataTable();
                dtTableType.TableName = "TableType";

                //We define another DataTable dtTableTypeCopy to avoid issue related to DateTime conversion
                DataTable dtTableTypeCopy = new DataTable();
                dtTableTypeCopy.TableName = "TableType";

                #region Define columns for dtTableTypeCopy
                DataColumn dtColumnTableTypeIdFordtTableTypeCopy = new DataColumn();
                    dtColumnTableTypeIdFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnTableTypeIdFordtTableTypeCopy.ColumnName = "TableTypeId";
                    dtTableTypeCopy.Columns.Add(dtColumnTableTypeIdFordtTableTypeCopy);

                    DataColumn dtColumnActiveFordtTableTypeCopy = new DataColumn();
                    dtColumnActiveFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnActiveFordtTableTypeCopy.ColumnName = "Active";
                    dtTableTypeCopy.Columns.Add(dtColumnActiveFordtTableTypeCopy);

                    DataColumn dtColumnDateTimeCreationFordtTableTypeCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTableTypeCopy.ColumnName = "DateTimeCreation";
                    dtTableTypeCopy.Columns.Add(dtColumnDateTimeCreationFordtTableTypeCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTableTypeCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTableTypeCopy.ColumnName = "DateTimeLastModification";
                    dtTableTypeCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTableTypeCopy);

                    DataColumn dtColumnUserCreationIdFordtTableTypeCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTableTypeCopy.ColumnName = "UserCreationId";
                    dtTableTypeCopy.Columns.Add(dtColumnUserCreationIdFordtTableTypeCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTableTypeCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTableTypeCopy.ColumnName = "UserLastModificationId";
                    dtTableTypeCopy.Columns.Add(dtColumnUserLastModificationIdFordtTableTypeCopy);

                    DataColumn dtColumnNameFordtTableTypeCopy = new DataColumn();
                    dtColumnNameFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnNameFordtTableTypeCopy.ColumnName = "Name";
                    dtTableTypeCopy.Columns.Add(dtColumnNameFordtTableTypeCopy);

                    
                #endregion

                dtTableType = new TableTypeModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtTableType.Rows)
                {
                    dtTableTypeCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtTableTypeCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/TableTypeing/TableType/TableType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsTableType = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtTableTypeCopy to avoid issue related to DateTime conversion
                    DataTable dtTableTypeCopy = new DataTable();
                    dtTableTypeCopy.TableName = "TableType";

                    #region Define columns for dtTableTypeCopy
                    DataColumn dtColumnTableTypeIdFordtTableTypeCopy = new DataColumn();
                    dtColumnTableTypeIdFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnTableTypeIdFordtTableTypeCopy.ColumnName = "TableTypeId";
                    dtTableTypeCopy.Columns.Add(dtColumnTableTypeIdFordtTableTypeCopy);

                    DataColumn dtColumnActiveFordtTableTypeCopy = new DataColumn();
                    dtColumnActiveFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnActiveFordtTableTypeCopy.ColumnName = "Active";
                    dtTableTypeCopy.Columns.Add(dtColumnActiveFordtTableTypeCopy);

                    DataColumn dtColumnDateTimeCreationFordtTableTypeCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTableTypeCopy.ColumnName = "DateTimeCreation";
                    dtTableTypeCopy.Columns.Add(dtColumnDateTimeCreationFordtTableTypeCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTableTypeCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTableTypeCopy.ColumnName = "DateTimeLastModification";
                    dtTableTypeCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTableTypeCopy);

                    DataColumn dtColumnUserCreationIdFordtTableTypeCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTableTypeCopy.ColumnName = "UserCreationId";
                    dtTableTypeCopy.Columns.Add(dtColumnUserCreationIdFordtTableTypeCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTableTypeCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTableTypeCopy.ColumnName = "UserLastModificationId";
                    dtTableTypeCopy.Columns.Add(dtColumnUserLastModificationIdFordtTableTypeCopy);

                    DataColumn dtColumnNameFordtTableTypeCopy = new DataColumn();
                    dtColumnNameFordtTableTypeCopy.DataType = typeof(string);
                    dtColumnNameFordtTableTypeCopy.ColumnName = "Name";
                    dtTableTypeCopy.Columns.Add(dtColumnNameFordtTableTypeCopy);

                    
                    #endregion

                    dsTableType.Tables.Add(dtTableTypeCopy);

                    for (int i = 0; i < dsTableType.Tables.Count; i++)
                    {
                        dtTableTypeCopy = new TableTypeModel().Select1ByTableTypeIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtTableTypeCopy.Rows)
                        {
                            dsTableType.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsTableType.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsTableType.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/TableTypeing/TableType/TableType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<TableTypeModel> lstTableTypeModel = new List<TableTypeModel> { };

            if (ExportationType == "All")
            {
                lstTableTypeModel = new TableTypeModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TableTypeModel TableTypeModel = new TableTypeModel().Select1ByTableTypeIdToModel(Convert.ToInt32(RowChecked));
                    lstTableTypeModel.Add(TableTypeModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/TableTypeing/TableType/TableType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstTableTypeModel);
            }

            return Now;
        }
        #endregion
    }
}