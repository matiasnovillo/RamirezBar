using Dapper;
using RamirezBar.Areas.RamirezBar.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

namespace RamirezBar.Areas.RamirezBar.Models
{
    /// <summary>
    /// Stack:             3 <br/>
    /// Name:              C# Model with stored procedure calls saved on database. <br/>
    /// Function:          Allow you to manipulate information from database using stored procedures.
    ///                    Also, let you make other related actions with the model in question or
    ///                    make temporal copies with random data. <br/>
    /// Fields:            11 <br/> 
    /// Sub-models:      0 models <br/>
    /// Last modification: 21/08/2023 6:56:48
    /// </summary>
    [Serializable]
    public partial class TableModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("TableId")]
        public int TableId { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        public bool Active { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.DateTime("DateTimeCreation", false, "1753-01-01T00:00", "9998-12-30T23:59")]
        public DateTime DateTimeCreation { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.DateTime("DateTimeLastModification", false, "1753-01-01T00:00", "9998-12-30T23:59")]
        public DateTime DateTimeLastModification { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.Key("UserCreationId")]
        public int UserCreationId { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.Key("UserLastModificationId")]
        public int UserLastModificationId { get; set; }

        [Library.ModelAttributeValidator.String("Name", true, 1, 50, "")]
        public string Name { get; set; }

        [Library.ModelAttributeValidator.String("Photo", false, 1, 8000, "")]
        public string Photo { get; set; }

        [Library.ModelAttributeValidator.Key("UserWaiterId")]
        public int UserWaiterId { get; set; }

        [Library.ModelAttributeValidator.Key("TableStateId")]
        public int TableStateId { get; set; }

        [Library.ModelAttributeValidator.Int("WinningMoney", false, 0, 999999)]
        public int WinningMoney { get; set; }

        public string TableStateName { get; set; }

        public string UserWaiterFantasyName { get; set; }
        #endregion

        #region Sub-lists

        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field TableId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public TableModel()
        {
            try 
            {
                TableId = 0;

                //Initialize sub-lists
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using TableId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public TableModel(int TableId)
        {
            try
            {
                List<TableModel> lstTableModel = new List<TableModel>();

                //Initialize sub-lists
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<TableModel>
                    lstTableModel = (List<TableModel>)sqlConnection.Query<TableModel>("[dbo].[RamirezBar.Table.Select1ByTableId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstTableModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[RamirezBar.Table.Select1ByTableId] returned more than one register/row");
                }
        
                foreach (TableModel table in lstTableModel)
                {
                    this.TableId = table.TableId;
					this.Active = table.Active;
					this.DateTimeCreation = table.DateTimeCreation;
					this.DateTimeLastModification = table.DateTimeLastModification;
					this.UserCreationId = table.UserCreationId;
					this.UserLastModificationId = table.UserLastModificationId;
					this.Name = table.Name;
					this.Photo = table.Photo;
					this.UserWaiterId = table.UserWaiterId;
					this.TableStateId = table.TableStateId;
					this.WinningMoney = table.WinningMoney;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public TableModel(int TableId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Name, string Photo, int UserWaiterId, int TableStateId, int WinningMoney)
        {
            try
            {
                //Initialize sub-lists
                

                this.TableId = TableId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.Name = Name;
				this.Photo = Photo;
				this.UserWaiterId = UserWaiterId;
				this.TableStateId = TableStateId;
				this.WinningMoney = WinningMoney;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), table, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public TableModel(TableModel table)
        {
            try
            {
                //Initialize sub-lists
                

                TableId = table.TableId;
				Active = table.Active;
				DateTimeCreation = table.DateTimeCreation;
				DateTimeLastModification = table.DateTimeLastModification;
				UserCreationId = table.UserCreationId;
				UserLastModificationId = table.UserLastModificationId;
				Name = table.Name;
				Photo = table.Photo;
				UserWaiterId = table.UserWaiterId;
				TableStateId = table.TableStateId;
				WinningMoney = table.WinningMoney;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Table</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.Count]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }

                int RowsCounter = Convert.ToInt32(DataTable.Rows[0][0].ToString());

                return RowsCounter;
            }
            catch (Exception ex) { throw ex; }
        }

        #region Queries to DataTable
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public DataTable Select1ByTableIdToDataTable(int TableId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.Select1ByTableId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[RamirezBar.Table.Select1ByTableId] returned more than one register/row"); }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectAllToDataTable()
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Queries to Models
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public TableModel Select1ByTableIdToModel(int TableId)
        {
            try
            {
                TableModel TableModel = new TableModel();
                List<TableModel> lstTableModel = new List<TableModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstTableModel = (List<TableModel>)sqlConnection.Query<TableModel>("[dbo].[RamirezBar.Table.Select1ByTableId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstTableModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[RamirezBar.Table.Select1ByTableId] returned more than one register/row"); }

                foreach (TableModel table in lstTableModel)
                {
                    TableModel.TableId = table.TableId;
					TableModel.Active = table.Active;
					TableModel.DateTimeCreation = table.DateTimeCreation;
					TableModel.DateTimeLastModification = table.DateTimeLastModification;
					TableModel.UserCreationId = table.UserCreationId;
					TableModel.UserLastModificationId = table.UserLastModificationId;
					TableModel.Name = table.Name;
					TableModel.Photo = table.Photo;
					TableModel.UserWaiterId = table.UserWaiterId;
					TableModel.TableStateId = table.TableStateId;
					TableModel.WinningMoney = table.WinningMoney;
                }

                return TableModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<TableModel> SelectAllToList()
        {
            try
            {
                List<TableModel> lstTableModel = new List<TableModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstTableModel = (List<TableModel>)sqlConnection.Query<TableModel>("[dbo].[RamirezBar.Table.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstTableModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public tableSelectAllPaged SelectAllPagedToModel(tableSelectAllPaged tableSelectAllPaged)
        {
            try
            {
                tableSelectAllPaged.lstTableModel = new List<TableModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", tableSelectAllPaged.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", tableSelectAllPaged.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", tableSelectAllPaged.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", tableSelectAllPaged.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", tableSelectAllPaged.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", tableSelectAllPaged.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    tableSelectAllPaged.lstTableModel = (List<TableModel>)sqlConnection.Query<TableModel>("[dbo].[RamirezBar.Table.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    tableSelectAllPaged.TotalRows = dp.Get<int>("TotalRows");
                }

                tableSelectAllPaged.TotalPages = Library.Math.Divide(tableSelectAllPaged.TotalRows, tableSelectAllPaged.RowsPerPage, Library.Math.RoundType.RoundUp);

                

                return tableSelectAllPaged;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Table table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Photo", Photo, DbType.String, ParameterDirection.Input);
				dp.Add("UserWaiterId", UserWaiterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("TableStateId", TableStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("WinningMoney", WinningMoney, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Table table</returns>
        public int Insert(TableModel table)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", table.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", table.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", table.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", table.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", table.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", table.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Photo", table.Photo, DbType.String, ParameterDirection.Input);
				dp.Add("UserWaiterId", table.UserWaiterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("TableStateId", table.TableStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("WinningMoney", table.WinningMoney, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Table table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Name, string Photo, int UserWaiterId, int TableStateId, int WinningMoney)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Photo", Photo, DbType.String, ParameterDirection.Input);
				dp.Add("UserWaiterId", UserWaiterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("TableStateId", TableStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("WinningMoney", WinningMoney, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Table table</returns>
        public int UpdateByTableId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Photo", Photo, DbType.String, ParameterDirection.Input);
				dp.Add("UserWaiterId", UserWaiterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("TableStateId", TableStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("WinningMoney", WinningMoney, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.UpdateByTableId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Table table</returns>
        public int UpdateByTableId(TableModel table)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("TableId", table.TableId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", table.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", table.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", table.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", table.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", table.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", table.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Photo", table.Photo, DbType.String, ParameterDirection.Input);
				dp.Add("UserWaiterId", table.UserWaiterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("TableStateId", table.TableStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("WinningMoney", table.WinningMoney, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.UpdateByTableId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Table table</returns>
        public int UpdateByTableId(int TableId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Name, string Photo, int UserWaiterId, int TableStateId, int WinningMoney)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Photo", Photo, DbType.String, ParameterDirection.Input);
				dp.Add("UserWaiterId", UserWaiterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("TableStateId", TableStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("WinningMoney", WinningMoney, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.UpdateByTableId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        ///
        public void DeleteAll()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Table table</returns>
        public int DeleteByTableId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.DeleteByTableId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows affected in Table table</returns>
        public int DeleteByTableId(int TableId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("TableId", TableId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[RamirezBar.Table.DeleteByTableId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// <summary>
        /// Function: Take the model stored in the given byte array to return the model. <br/>
        /// Note 1:   Similar to a decryptor function. <br/>
        /// Note 2:   The model need the [Serializable] decorator in model definition. <br/>
        /// </summary>
        public TableModel ByteArrayToTableModel<T>(byte[] arrTableModel)
        {
            try
            {
                if (arrTableModel == null)
                { return new TableModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrTableModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (TableModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"TableId: {TableId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"Name: {Name}, " +
				$"Photo: {Photo}, " +
				$"UserWaiterId: {UserWaiterId}, " +
				$"TableStateId: {TableStateId}, " +
				$"WinningMoney: {WinningMoney}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{TableId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Name}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Photo}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserWaiterId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{TableStateId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{WinningMoney}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }
}