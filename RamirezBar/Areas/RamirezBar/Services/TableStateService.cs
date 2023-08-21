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

//Last modification on: 20/08/2023 23:27:19

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/08/2023 23:27:19
    /// </summary>
    public partial class TableStateService : ITableState
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public TableStateService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public TableStateModel Select1ByTableStateIdToModel(int TableStateId)
        {
            return new TableStateModel().Select1ByTableStateIdToModel(TableStateId);
        }

        public List<TableStateModel> SelectAllToList()
        {
            return new TableStateModel().SelectAllToList();
        }

        public tablestateSelectAllPaged SelectAllPagedToModel(tablestateSelectAllPaged tablestateSelectAllPaged)
        {
            return new TableStateModel().SelectAllPagedToModel(tablestateSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(TableStateModel TableStateModel)
        {
            return new TableStateModel().Insert(TableStateModel);
        }

        public int UpdateByTableStateId(TableStateModel TableStateModel)
        {
            return new TableStateModel().UpdateByTableStateId(TableStateModel);
        }

        public int DeleteByTableStateId(int TableStateId)
        {
            return new TableStateModel().DeleteByTableStateId(TableStateId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                TableStateModel TableStateModel = new TableStateModel();
                TableStateModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TableStateModel TableStateModel = new TableStateModel().Select1ByTableStateIdToModel(Convert.ToInt32(RowsChecked[i]));
                    TableStateModel.DeleteByTableStateId(TableStateModel.TableStateId);
                }
            }
        }

        public int CopyByTableStateId(int TableStateId)
        {
            TableStateModel TableStateModel = new TableStateModel().Select1ByTableStateIdToModel(TableStateId);
            int NewEnteredId = new TableStateModel().Insert(TableStateModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<TableStateModel> lstTableStateModel = new List<TableStateModel> { };
                lstTableStateModel = new TableStateModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstTableStateModel.Count];

                for (int i = 0; i < lstTableStateModel.Count; i++)
                {
                    NewEnteredIds[i] = lstTableStateModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TableStateModel TableStateModel = new TableStateModel().Select1ByTableStateIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = TableStateModel.Insert();
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
            List<TableStateModel> lstTableStateModel = new List<TableStateModel> { };

            if (ExportationType == "All")
            {
                lstTableStateModel = new TableStateModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TableStateModel TableStateModel = new TableStateModel().Select1ByTableStateIdToModel(Convert.ToInt32(RowChecked));
                    lstTableStateModel.Add(TableStateModel);
                }
            }

            foreach (TableStateModel row in lstTableStateModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of TableState</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TableStateId&nbsp;&nbsp;&nbsp;</span>
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
        </th>
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {Now}</span>
</font>
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/TableState/TableState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtTableState = new DataTable();
                dtTableState.TableName = "TableState";

                //We define another DataTable dtTableStateCopy to avoid issue related to DateTime conversion
                DataTable dtTableStateCopy = new DataTable();
                dtTableStateCopy.TableName = "TableState";

                #region Define columns for dtTableStateCopy
                DataColumn dtColumnTableStateIdFordtTableStateCopy = new DataColumn();
                    dtColumnTableStateIdFordtTableStateCopy.DataType = typeof(string);
                    dtColumnTableStateIdFordtTableStateCopy.ColumnName = "TableStateId";
                    dtTableStateCopy.Columns.Add(dtColumnTableStateIdFordtTableStateCopy);

                    DataColumn dtColumnActiveFordtTableStateCopy = new DataColumn();
                    dtColumnActiveFordtTableStateCopy.DataType = typeof(string);
                    dtColumnActiveFordtTableStateCopy.ColumnName = "Active";
                    dtTableStateCopy.Columns.Add(dtColumnActiveFordtTableStateCopy);

                    DataColumn dtColumnDateTimeCreationFordtTableStateCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTableStateCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTableStateCopy.ColumnName = "DateTimeCreation";
                    dtTableStateCopy.Columns.Add(dtColumnDateTimeCreationFordtTableStateCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTableStateCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTableStateCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTableStateCopy.ColumnName = "DateTimeLastModification";
                    dtTableStateCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTableStateCopy);

                    DataColumn dtColumnUserCreationIdFordtTableStateCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTableStateCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTableStateCopy.ColumnName = "UserCreationId";
                    dtTableStateCopy.Columns.Add(dtColumnUserCreationIdFordtTableStateCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTableStateCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTableStateCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTableStateCopy.ColumnName = "UserLastModificationId";
                    dtTableStateCopy.Columns.Add(dtColumnUserLastModificationIdFordtTableStateCopy);

                    
                #endregion

                dtTableState = new TableStateModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtTableState.Rows)
                {
                    dtTableStateCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtTableStateCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/TableStateing/TableState/TableState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsTableState = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtTableStateCopy to avoid issue related to DateTime conversion
                    DataTable dtTableStateCopy = new DataTable();
                    dtTableStateCopy.TableName = "TableState";

                    #region Define columns for dtTableStateCopy
                    DataColumn dtColumnTableStateIdFordtTableStateCopy = new DataColumn();
                    dtColumnTableStateIdFordtTableStateCopy.DataType = typeof(string);
                    dtColumnTableStateIdFordtTableStateCopy.ColumnName = "TableStateId";
                    dtTableStateCopy.Columns.Add(dtColumnTableStateIdFordtTableStateCopy);

                    DataColumn dtColumnActiveFordtTableStateCopy = new DataColumn();
                    dtColumnActiveFordtTableStateCopy.DataType = typeof(string);
                    dtColumnActiveFordtTableStateCopy.ColumnName = "Active";
                    dtTableStateCopy.Columns.Add(dtColumnActiveFordtTableStateCopy);

                    DataColumn dtColumnDateTimeCreationFordtTableStateCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTableStateCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTableStateCopy.ColumnName = "DateTimeCreation";
                    dtTableStateCopy.Columns.Add(dtColumnDateTimeCreationFordtTableStateCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTableStateCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTableStateCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTableStateCopy.ColumnName = "DateTimeLastModification";
                    dtTableStateCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTableStateCopy);

                    DataColumn dtColumnUserCreationIdFordtTableStateCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTableStateCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTableStateCopy.ColumnName = "UserCreationId";
                    dtTableStateCopy.Columns.Add(dtColumnUserCreationIdFordtTableStateCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTableStateCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTableStateCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTableStateCopy.ColumnName = "UserLastModificationId";
                    dtTableStateCopy.Columns.Add(dtColumnUserLastModificationIdFordtTableStateCopy);

                    
                    #endregion

                    dsTableState.Tables.Add(dtTableStateCopy);

                    for (int i = 0; i < dsTableState.Tables.Count; i++)
                    {
                        dtTableStateCopy = new TableStateModel().Select1ByTableStateIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtTableStateCopy.Rows)
                        {
                            dsTableState.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsTableState.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsTableState.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/TableStateing/TableState/TableState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<TableStateModel> lstTableStateModel = new List<TableStateModel> { };

            if (ExportationType == "All")
            {
                lstTableStateModel = new TableStateModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TableStateModel TableStateModel = new TableStateModel().Select1ByTableStateIdToModel(Convert.ToInt32(RowChecked));
                    lstTableStateModel.Add(TableStateModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/TableStateing/TableState/TableState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstTableStateModel);
            }

            return Now;
        }
        #endregion
    }
}