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

//Last modification on: 06/11/2023 14:38:45

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 06/11/2023 14:38:45
    /// </summary>
    public partial class PointService : IPoint
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public PointService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public PointModel Select1ByPointIdToModel(int PointId)
        {
            return new PointModel().Select1ByPointIdToModel(PointId);
        }

        public List<PointModel> SelectAllToList()
        {
            return new PointModel().SelectAllToList();
        }

        public pointSelectAllPaged SelectAllPagedToModel(pointSelectAllPaged pointSelectAllPaged)
        {
            return new PointModel().SelectAllPagedToModel(pointSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(PointModel PointModel)
        {
            return new PointModel().Insert(PointModel);
        }

        public int UpdateByPointId(PointModel PointModel)
        {
            return new PointModel().UpdateByPointId(PointModel);
        }

        public int DeleteByPointId(int PointId)
        {
            return new PointModel().DeleteByPointId(PointId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                PointModel PointModel = new PointModel();
                PointModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    PointModel PointModel = new PointModel().Select1ByPointIdToModel(Convert.ToInt32(RowsChecked[i]));
                    PointModel.DeleteByPointId(PointModel.PointId);
                }
            }
        }

        public int CopyByPointId(int PointId)
        {
            PointModel PointModel = new PointModel().Select1ByPointIdToModel(PointId);
            int NewEnteredId = new PointModel().Insert(PointModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<PointModel> lstPointModel = new List<PointModel> { };
                lstPointModel = new PointModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstPointModel.Count];

                for (int i = 0; i < lstPointModel.Count; i++)
                {
                    NewEnteredIds[i] = lstPointModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    PointModel PointModel = new PointModel().Select1ByPointIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = PointModel.Insert();
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
            List<PointModel> lstPointModel = new List<PointModel> { };

            if (ExportationType == "All")
            {
                lstPointModel = new PointModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    PointModel PointModel = new PointModel().Select1ByPointIdToModel(Convert.ToInt32(RowChecked));
                    lstPointModel.Add(PointModel);
                }
            }

            foreach (PointModel row in lstPointModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Point</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PointId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DNI&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Point&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/Point/Point_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtPoint = new DataTable();
                dtPoint.TableName = "Point";

                //We define another DataTable dtPointCopy to avoid issue related to DateTime conversion
                DataTable dtPointCopy = new DataTable();
                dtPointCopy.TableName = "Point";

                #region Define columns for dtPointCopy
                DataColumn dtColumnPointIdFordtPointCopy = new DataColumn();
                    dtColumnPointIdFordtPointCopy.DataType = typeof(string);
                    dtColumnPointIdFordtPointCopy.ColumnName = "PointId";
                    dtPointCopy.Columns.Add(dtColumnPointIdFordtPointCopy);

                    DataColumn dtColumnActiveFordtPointCopy = new DataColumn();
                    dtColumnActiveFordtPointCopy.DataType = typeof(string);
                    dtColumnActiveFordtPointCopy.ColumnName = "Active";
                    dtPointCopy.Columns.Add(dtColumnActiveFordtPointCopy);

                    DataColumn dtColumnDateTimeCreationFordtPointCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtPointCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtPointCopy.ColumnName = "DateTimeCreation";
                    dtPointCopy.Columns.Add(dtColumnDateTimeCreationFordtPointCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtPointCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtPointCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtPointCopy.ColumnName = "DateTimeLastModification";
                    dtPointCopy.Columns.Add(dtColumnDateTimeLastModificationFordtPointCopy);

                    DataColumn dtColumnUserCreationIdFordtPointCopy = new DataColumn();
                    dtColumnUserCreationIdFordtPointCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtPointCopy.ColumnName = "UserCreationId";
                    dtPointCopy.Columns.Add(dtColumnUserCreationIdFordtPointCopy);

                    DataColumn dtColumnUserLastModificationIdFordtPointCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtPointCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtPointCopy.ColumnName = "UserLastModificationId";
                    dtPointCopy.Columns.Add(dtColumnUserLastModificationIdFordtPointCopy);

                    DataColumn dtColumnDNIFordtPointCopy = new DataColumn();
                    dtColumnDNIFordtPointCopy.DataType = typeof(string);
                    dtColumnDNIFordtPointCopy.ColumnName = "DNI";
                    dtPointCopy.Columns.Add(dtColumnDNIFordtPointCopy);

                    DataColumn dtColumnPointFordtPointCopy = new DataColumn();
                    dtColumnPointFordtPointCopy.DataType = typeof(string);
                    dtColumnPointFordtPointCopy.ColumnName = "Point";
                    dtPointCopy.Columns.Add(dtColumnPointFordtPointCopy);

                    
                #endregion

                dtPoint = new PointModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtPoint.Rows)
                {
                    dtPointCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtPointCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Pointing/Point/Point_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsPoint = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtPointCopy to avoid issue related to DateTime conversion
                    DataTable dtPointCopy = new DataTable();
                    dtPointCopy.TableName = "Point";

                    #region Define columns for dtPointCopy
                    DataColumn dtColumnPointIdFordtPointCopy = new DataColumn();
                    dtColumnPointIdFordtPointCopy.DataType = typeof(string);
                    dtColumnPointIdFordtPointCopy.ColumnName = "PointId";
                    dtPointCopy.Columns.Add(dtColumnPointIdFordtPointCopy);

                    DataColumn dtColumnActiveFordtPointCopy = new DataColumn();
                    dtColumnActiveFordtPointCopy.DataType = typeof(string);
                    dtColumnActiveFordtPointCopy.ColumnName = "Active";
                    dtPointCopy.Columns.Add(dtColumnActiveFordtPointCopy);

                    DataColumn dtColumnDateTimeCreationFordtPointCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtPointCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtPointCopy.ColumnName = "DateTimeCreation";
                    dtPointCopy.Columns.Add(dtColumnDateTimeCreationFordtPointCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtPointCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtPointCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtPointCopy.ColumnName = "DateTimeLastModification";
                    dtPointCopy.Columns.Add(dtColumnDateTimeLastModificationFordtPointCopy);

                    DataColumn dtColumnUserCreationIdFordtPointCopy = new DataColumn();
                    dtColumnUserCreationIdFordtPointCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtPointCopy.ColumnName = "UserCreationId";
                    dtPointCopy.Columns.Add(dtColumnUserCreationIdFordtPointCopy);

                    DataColumn dtColumnUserLastModificationIdFordtPointCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtPointCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtPointCopy.ColumnName = "UserLastModificationId";
                    dtPointCopy.Columns.Add(dtColumnUserLastModificationIdFordtPointCopy);

                    DataColumn dtColumnDNIFordtPointCopy = new DataColumn();
                    dtColumnDNIFordtPointCopy.DataType = typeof(string);
                    dtColumnDNIFordtPointCopy.ColumnName = "DNI";
                    dtPointCopy.Columns.Add(dtColumnDNIFordtPointCopy);

                    DataColumn dtColumnPointFordtPointCopy = new DataColumn();
                    dtColumnPointFordtPointCopy.DataType = typeof(string);
                    dtColumnPointFordtPointCopy.ColumnName = "Point";
                    dtPointCopy.Columns.Add(dtColumnPointFordtPointCopy);

                    
                    #endregion

                    dsPoint.Tables.Add(dtPointCopy);

                    for (int i = 0; i < dsPoint.Tables.Count; i++)
                    {
                        dtPointCopy = new PointModel().Select1ByPointIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtPointCopy.Rows)
                        {
                            dsPoint.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsPoint.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsPoint.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Pointing/Point/Point_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<PointModel> lstPointModel = new List<PointModel> { };

            if (ExportationType == "All")
            {
                lstPointModel = new PointModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    PointModel PointModel = new PointModel().Select1ByPointIdToModel(Convert.ToInt32(RowChecked));
                    lstPointModel.Add(PointModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Pointing/Point/Point_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstPointModel);
            }

            return Now;
        }
        #endregion
    }
}