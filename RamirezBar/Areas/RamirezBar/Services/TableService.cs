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

//Last modification on: 20/08/2023 23:27:14

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/08/2023 23:27:14
    /// </summary>
    public partial class TableService : ITable
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public TableService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public TableModel Select1ByTableIdToModel(int TableId)
        {
            return new TableModel().Select1ByTableIdToModel(TableId);
        }

        public List<TableModel> SelectAllToList()
        {
            return new TableModel().SelectAllToList();
        }

        public tableSelectAllPaged SelectAllPagedToModel(tableSelectAllPaged tableSelectAllPaged)
        {
            return new TableModel().SelectAllPagedToModel(tableSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(TableModel TableModel)
        {
            return new TableModel().Insert(TableModel);
        }

        public int UpdateByTableId(TableModel TableModel)
        {
            return new TableModel().UpdateByTableId(TableModel);
        }

        public int DeleteByTableId(int TableId)
        {
            return new TableModel().DeleteByTableId(TableId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                TableModel TableModel = new TableModel();
                TableModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TableModel TableModel = new TableModel().Select1ByTableIdToModel(Convert.ToInt32(RowsChecked[i]));
                    TableModel.DeleteByTableId(TableModel.TableId);
                }
            }
        }

        public int CopyByTableId(int TableId)
        {
            TableModel TableModel = new TableModel().Select1ByTableIdToModel(TableId);
            int NewEnteredId = new TableModel().Insert(TableModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<TableModel> lstTableModel = new List<TableModel> { };
                lstTableModel = new TableModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstTableModel.Count];

                for (int i = 0; i < lstTableModel.Count; i++)
                {
                    NewEnteredIds[i] = lstTableModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TableModel TableModel = new TableModel().Select1ByTableIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = TableModel.Insert();
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
            List<TableModel> lstTableModel = new List<TableModel> { };

            if (ExportationType == "All")
            {
                lstTableModel = new TableModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TableModel TableModel = new TableModel().Select1ByTableIdToModel(Convert.ToInt32(RowChecked));
                    lstTableModel.Add(TableModel);
                }
            }

            foreach (TableModel row in lstTableModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Table</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TableId&nbsp;&nbsp;&nbsp;</span>
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
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Photo&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserWaiterId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TableStateId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/Table/Table_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtTable = new DataTable();
                dtTable.TableName = "Table";

                //We define another DataTable dtTableCopy to avoid issue related to DateTime conversion
                DataTable dtTableCopy = new DataTable();
                dtTableCopy.TableName = "Table";

                #region Define columns for dtTableCopy
                DataColumn dtColumnTableIdFordtTableCopy = new DataColumn();
                    dtColumnTableIdFordtTableCopy.DataType = typeof(string);
                    dtColumnTableIdFordtTableCopy.ColumnName = "TableId";
                    dtTableCopy.Columns.Add(dtColumnTableIdFordtTableCopy);

                    DataColumn dtColumnActiveFordtTableCopy = new DataColumn();
                    dtColumnActiveFordtTableCopy.DataType = typeof(string);
                    dtColumnActiveFordtTableCopy.ColumnName = "Active";
                    dtTableCopy.Columns.Add(dtColumnActiveFordtTableCopy);

                    DataColumn dtColumnDateTimeCreationFordtTableCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTableCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTableCopy.ColumnName = "DateTimeCreation";
                    dtTableCopy.Columns.Add(dtColumnDateTimeCreationFordtTableCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTableCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTableCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTableCopy.ColumnName = "DateTimeLastModification";
                    dtTableCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTableCopy);

                    DataColumn dtColumnUserCreationIdFordtTableCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTableCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTableCopy.ColumnName = "UserCreationId";
                    dtTableCopy.Columns.Add(dtColumnUserCreationIdFordtTableCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTableCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTableCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTableCopy.ColumnName = "UserLastModificationId";
                    dtTableCopy.Columns.Add(dtColumnUserLastModificationIdFordtTableCopy);

                    DataColumn dtColumnNameFordtTableCopy = new DataColumn();
                    dtColumnNameFordtTableCopy.DataType = typeof(string);
                    dtColumnNameFordtTableCopy.ColumnName = "Name";
                    dtTableCopy.Columns.Add(dtColumnNameFordtTableCopy);

                    DataColumn dtColumnPhotoFordtTableCopy = new DataColumn();
                    dtColumnPhotoFordtTableCopy.DataType = typeof(string);
                    dtColumnPhotoFordtTableCopy.ColumnName = "Photo";
                    dtTableCopy.Columns.Add(dtColumnPhotoFordtTableCopy);

                    DataColumn dtColumnUserWaiterIdFordtTableCopy = new DataColumn();
                    dtColumnUserWaiterIdFordtTableCopy.DataType = typeof(string);
                    dtColumnUserWaiterIdFordtTableCopy.ColumnName = "UserWaiterId";
                    dtTableCopy.Columns.Add(dtColumnUserWaiterIdFordtTableCopy);

                    DataColumn dtColumnTableStateIdFordtTableCopy = new DataColumn();
                    dtColumnTableStateIdFordtTableCopy.DataType = typeof(string);
                    dtColumnTableStateIdFordtTableCopy.ColumnName = "TableStateId";
                    dtTableCopy.Columns.Add(dtColumnTableStateIdFordtTableCopy);

                    
                #endregion

                dtTable = new TableModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtTable.Rows)
                {
                    dtTableCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtTableCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Tableing/Table/Table_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsTable = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtTableCopy to avoid issue related to DateTime conversion
                    DataTable dtTableCopy = new DataTable();
                    dtTableCopy.TableName = "Table";

                    #region Define columns for dtTableCopy
                    DataColumn dtColumnTableIdFordtTableCopy = new DataColumn();
                    dtColumnTableIdFordtTableCopy.DataType = typeof(string);
                    dtColumnTableIdFordtTableCopy.ColumnName = "TableId";
                    dtTableCopy.Columns.Add(dtColumnTableIdFordtTableCopy);

                    DataColumn dtColumnActiveFordtTableCopy = new DataColumn();
                    dtColumnActiveFordtTableCopy.DataType = typeof(string);
                    dtColumnActiveFordtTableCopy.ColumnName = "Active";
                    dtTableCopy.Columns.Add(dtColumnActiveFordtTableCopy);

                    DataColumn dtColumnDateTimeCreationFordtTableCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTableCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTableCopy.ColumnName = "DateTimeCreation";
                    dtTableCopy.Columns.Add(dtColumnDateTimeCreationFordtTableCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTableCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTableCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTableCopy.ColumnName = "DateTimeLastModification";
                    dtTableCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTableCopy);

                    DataColumn dtColumnUserCreationIdFordtTableCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTableCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTableCopy.ColumnName = "UserCreationId";
                    dtTableCopy.Columns.Add(dtColumnUserCreationIdFordtTableCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTableCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTableCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTableCopy.ColumnName = "UserLastModificationId";
                    dtTableCopy.Columns.Add(dtColumnUserLastModificationIdFordtTableCopy);

                    DataColumn dtColumnNameFordtTableCopy = new DataColumn();
                    dtColumnNameFordtTableCopy.DataType = typeof(string);
                    dtColumnNameFordtTableCopy.ColumnName = "Name";
                    dtTableCopy.Columns.Add(dtColumnNameFordtTableCopy);

                    DataColumn dtColumnPhotoFordtTableCopy = new DataColumn();
                    dtColumnPhotoFordtTableCopy.DataType = typeof(string);
                    dtColumnPhotoFordtTableCopy.ColumnName = "Photo";
                    dtTableCopy.Columns.Add(dtColumnPhotoFordtTableCopy);

                    DataColumn dtColumnUserWaiterIdFordtTableCopy = new DataColumn();
                    dtColumnUserWaiterIdFordtTableCopy.DataType = typeof(string);
                    dtColumnUserWaiterIdFordtTableCopy.ColumnName = "UserWaiterId";
                    dtTableCopy.Columns.Add(dtColumnUserWaiterIdFordtTableCopy);

                    DataColumn dtColumnTableStateIdFordtTableCopy = new DataColumn();
                    dtColumnTableStateIdFordtTableCopy.DataType = typeof(string);
                    dtColumnTableStateIdFordtTableCopy.ColumnName = "TableStateId";
                    dtTableCopy.Columns.Add(dtColumnTableStateIdFordtTableCopy);

                    
                    #endregion

                    dsTable.Tables.Add(dtTableCopy);

                    for (int i = 0; i < dsTable.Tables.Count; i++)
                    {
                        dtTableCopy = new TableModel().Select1ByTableIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtTableCopy.Rows)
                        {
                            dsTable.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsTable.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsTable.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Tableing/Table/Table_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<TableModel> lstTableModel = new List<TableModel> { };

            if (ExportationType == "All")
            {
                lstTableModel = new TableModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TableModel TableModel = new TableModel().Select1ByTableIdToModel(Convert.ToInt32(RowChecked));
                    lstTableModel.Add(TableModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Tableing/Table/Table_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstTableModel);
            }

            return Now;
        }
        #endregion
    }
}