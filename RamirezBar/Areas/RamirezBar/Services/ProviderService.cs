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

//Last modification on: 20/08/2023 23:02:48

namespace RamirezBar.Areas.RamirezBar.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/08/2023 23:02:48
    /// </summary>
    public partial class ProviderService : IProvider
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ProviderService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ProviderModel Select1ByProviderIdToModel(int ProviderId)
        {
            return new ProviderModel().Select1ByProviderIdToModel(ProviderId);
        }

        public List<ProviderModel> SelectAllToList()
        {
            return new ProviderModel().SelectAllToList();
        }

        public providerSelectAllPaged SelectAllPagedToModel(providerSelectAllPaged providerSelectAllPaged)
        {
            return new ProviderModel().SelectAllPagedToModel(providerSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ProviderModel ProviderModel)
        {
            return new ProviderModel().Insert(ProviderModel);
        }

        public int UpdateByProviderId(ProviderModel ProviderModel)
        {
            return new ProviderModel().UpdateByProviderId(ProviderModel);
        }

        public int DeleteByProviderId(int ProviderId)
        {
            return new ProviderModel().DeleteByProviderId(ProviderId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ProviderModel ProviderModel = new ProviderModel();
                ProviderModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ProviderModel ProviderModel = new ProviderModel().Select1ByProviderIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ProviderModel.DeleteByProviderId(ProviderModel.ProviderId);
                }
            }
        }

        public int CopyByProviderId(int ProviderId)
        {
            ProviderModel ProviderModel = new ProviderModel().Select1ByProviderIdToModel(ProviderId);
            int NewEnteredId = new ProviderModel().Insert(ProviderModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ProviderModel> lstProviderModel = new List<ProviderModel> { };
                lstProviderModel = new ProviderModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstProviderModel.Count];

                for (int i = 0; i < lstProviderModel.Count; i++)
                {
                    NewEnteredIds[i] = lstProviderModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ProviderModel ProviderModel = new ProviderModel().Select1ByProviderIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ProviderModel.Insert();
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
            List<ProviderModel> lstProviderModel = new List<ProviderModel> { };

            if (ExportationType == "All")
            {
                lstProviderModel = new ProviderModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ProviderModel ProviderModel = new ProviderModel().Select1ByProviderIdToModel(Convert.ToInt32(RowChecked));
                    lstProviderModel.Add(ProviderModel);
                }
            }

            foreach (ProviderModel row in lstProviderModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Provider</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProviderId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Address&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Phone1&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Phone2&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/RamirezBar/Provider/Provider_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtProvider = new DataTable();
                dtProvider.TableName = "Provider";

                //We define another DataTable dtProviderCopy to avoid issue related to DateTime conversion
                DataTable dtProviderCopy = new DataTable();
                dtProviderCopy.TableName = "Provider";

                #region Define columns for dtProviderCopy
                DataColumn dtColumnProviderIdFordtProviderCopy = new DataColumn();
                    dtColumnProviderIdFordtProviderCopy.DataType = typeof(string);
                    dtColumnProviderIdFordtProviderCopy.ColumnName = "ProviderId";
                    dtProviderCopy.Columns.Add(dtColumnProviderIdFordtProviderCopy);

                    DataColumn dtColumnActiveFordtProviderCopy = new DataColumn();
                    dtColumnActiveFordtProviderCopy.DataType = typeof(string);
                    dtColumnActiveFordtProviderCopy.ColumnName = "Active";
                    dtProviderCopy.Columns.Add(dtColumnActiveFordtProviderCopy);

                    DataColumn dtColumnDateTimeCreationFordtProviderCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtProviderCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtProviderCopy.ColumnName = "DateTimeCreation";
                    dtProviderCopy.Columns.Add(dtColumnDateTimeCreationFordtProviderCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtProviderCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtProviderCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtProviderCopy.ColumnName = "DateTimeLastModification";
                    dtProviderCopy.Columns.Add(dtColumnDateTimeLastModificationFordtProviderCopy);

                    DataColumn dtColumnUserCreationIdFordtProviderCopy = new DataColumn();
                    dtColumnUserCreationIdFordtProviderCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtProviderCopy.ColumnName = "UserCreationId";
                    dtProviderCopy.Columns.Add(dtColumnUserCreationIdFordtProviderCopy);

                    DataColumn dtColumnUserLastModificationIdFordtProviderCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtProviderCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtProviderCopy.ColumnName = "UserLastModificationId";
                    dtProviderCopy.Columns.Add(dtColumnUserLastModificationIdFordtProviderCopy);

                    DataColumn dtColumnNameFordtProviderCopy = new DataColumn();
                    dtColumnNameFordtProviderCopy.DataType = typeof(string);
                    dtColumnNameFordtProviderCopy.ColumnName = "Name";
                    dtProviderCopy.Columns.Add(dtColumnNameFordtProviderCopy);

                    DataColumn dtColumnAddressFordtProviderCopy = new DataColumn();
                    dtColumnAddressFordtProviderCopy.DataType = typeof(string);
                    dtColumnAddressFordtProviderCopy.ColumnName = "Address";
                    dtProviderCopy.Columns.Add(dtColumnAddressFordtProviderCopy);

                    DataColumn dtColumnPhone1FordtProviderCopy = new DataColumn();
                    dtColumnPhone1FordtProviderCopy.DataType = typeof(string);
                    dtColumnPhone1FordtProviderCopy.ColumnName = "Phone1";
                    dtProviderCopy.Columns.Add(dtColumnPhone1FordtProviderCopy);

                    DataColumn dtColumnPhone2FordtProviderCopy = new DataColumn();
                    dtColumnPhone2FordtProviderCopy.DataType = typeof(string);
                    dtColumnPhone2FordtProviderCopy.ColumnName = "Phone2";
                    dtProviderCopy.Columns.Add(dtColumnPhone2FordtProviderCopy);

                    
                #endregion

                dtProvider = new ProviderModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtProvider.Rows)
                {
                    dtProviderCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtProviderCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Providering/Provider/Provider_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsProvider = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtProviderCopy to avoid issue related to DateTime conversion
                    DataTable dtProviderCopy = new DataTable();
                    dtProviderCopy.TableName = "Provider";

                    #region Define columns for dtProviderCopy
                    DataColumn dtColumnProviderIdFordtProviderCopy = new DataColumn();
                    dtColumnProviderIdFordtProviderCopy.DataType = typeof(string);
                    dtColumnProviderIdFordtProviderCopy.ColumnName = "ProviderId";
                    dtProviderCopy.Columns.Add(dtColumnProviderIdFordtProviderCopy);

                    DataColumn dtColumnActiveFordtProviderCopy = new DataColumn();
                    dtColumnActiveFordtProviderCopy.DataType = typeof(string);
                    dtColumnActiveFordtProviderCopy.ColumnName = "Active";
                    dtProviderCopy.Columns.Add(dtColumnActiveFordtProviderCopy);

                    DataColumn dtColumnDateTimeCreationFordtProviderCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtProviderCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtProviderCopy.ColumnName = "DateTimeCreation";
                    dtProviderCopy.Columns.Add(dtColumnDateTimeCreationFordtProviderCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtProviderCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtProviderCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtProviderCopy.ColumnName = "DateTimeLastModification";
                    dtProviderCopy.Columns.Add(dtColumnDateTimeLastModificationFordtProviderCopy);

                    DataColumn dtColumnUserCreationIdFordtProviderCopy = new DataColumn();
                    dtColumnUserCreationIdFordtProviderCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtProviderCopy.ColumnName = "UserCreationId";
                    dtProviderCopy.Columns.Add(dtColumnUserCreationIdFordtProviderCopy);

                    DataColumn dtColumnUserLastModificationIdFordtProviderCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtProviderCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtProviderCopy.ColumnName = "UserLastModificationId";
                    dtProviderCopy.Columns.Add(dtColumnUserLastModificationIdFordtProviderCopy);

                    DataColumn dtColumnNameFordtProviderCopy = new DataColumn();
                    dtColumnNameFordtProviderCopy.DataType = typeof(string);
                    dtColumnNameFordtProviderCopy.ColumnName = "Name";
                    dtProviderCopy.Columns.Add(dtColumnNameFordtProviderCopy);

                    DataColumn dtColumnAddressFordtProviderCopy = new DataColumn();
                    dtColumnAddressFordtProviderCopy.DataType = typeof(string);
                    dtColumnAddressFordtProviderCopy.ColumnName = "Address";
                    dtProviderCopy.Columns.Add(dtColumnAddressFordtProviderCopy);

                    DataColumn dtColumnPhone1FordtProviderCopy = new DataColumn();
                    dtColumnPhone1FordtProviderCopy.DataType = typeof(string);
                    dtColumnPhone1FordtProviderCopy.ColumnName = "Phone1";
                    dtProviderCopy.Columns.Add(dtColumnPhone1FordtProviderCopy);

                    DataColumn dtColumnPhone2FordtProviderCopy = new DataColumn();
                    dtColumnPhone2FordtProviderCopy.DataType = typeof(string);
                    dtColumnPhone2FordtProviderCopy.ColumnName = "Phone2";
                    dtProviderCopy.Columns.Add(dtColumnPhone2FordtProviderCopy);

                    
                    #endregion

                    dsProvider.Tables.Add(dtProviderCopy);

                    for (int i = 0; i < dsProvider.Tables.Count; i++)
                    {
                        dtProviderCopy = new ProviderModel().Select1ByProviderIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtProviderCopy.Rows)
                        {
                            dsProvider.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsProvider.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsProvider.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Providering/Provider/Provider_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ProviderModel> lstProviderModel = new List<ProviderModel> { };

            if (ExportationType == "All")
            {
                lstProviderModel = new ProviderModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ProviderModel ProviderModel = new ProviderModel().Select1ByProviderIdToModel(Convert.ToInt32(RowChecked));
                    lstProviderModel.Add(ProviderModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Providering/Provider/Provider_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstProviderModel);
            }

            return Now;
        }
        #endregion
    }
}