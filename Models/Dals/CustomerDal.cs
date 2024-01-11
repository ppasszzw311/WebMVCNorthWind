﻿using Dapper;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace WebMvcNorthWind.Models.Dals
{
    public class CustomerDal : BaseDal
    {
        private readonly IConfiguration _configuration;

        public CustomerDal(IConfiguration configuration) : base(configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// 取得全部的顧客
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CustomerModel>> GetCustomer()
        {
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(@"SELECT * FROM Customers;");

                var result = await base._cn.QueryAsync<CustomerModel>(sbSql.ToString());

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 取得個別顧客
        /// </summary>
        /// <param name="p_sId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CustomerModel> GetCustomerByID(string p_sId)
        {
            StringBuilder sbSql = new StringBuilder();
            DynamicParameters p = new DynamicParameters();
            try
            {
                sbSql.Append("SELECT * FROM Customers WHERE CustomerID = @CustomerID;");
                p.Add("@CustomerID", p_sId);

                var oCustomer = await base._cn.QueryAsync<CustomerModel>(sbSql.ToString(), p);

                return (CustomerModel)oCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                AllDispose();
            }
        }


        /// <summary>
        /// 新增顧客
        /// </summary>
        /// <param name="p_oAdd"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> InsertCustomer(CustomerModel p_oAdd)
        {
            StringBuilder sbSql = new StringBuilder();
            DynamicParameters p = new DynamicParameters();
            try
            {

                sbSql.Append(@"INSERT INTO Customers 
                                  (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, 
                                   Region, PostalCode, Country, Phone, Fax) 
                               VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, 
                                   @Region, @PostalCode, @Country, @Phone, @Fax);"
                );

                p.Add("@CustomerID", p_oAdd.CustomerID);
                p.Add("@CompanyName", p_oAdd.CompanyName);
                p.Add("@ContactName", p_oAdd.ContactName);
                p.Add("@ContactTitle", p_oAdd.ContractTitle);
                p.Add("@Address", p_oAdd.Address);
                p.Add("@City", p_oAdd.City);
                p.Add("@Region", p_oAdd.Region);
                p.Add("@PostalCode", p_oAdd.PostalCode);
                p.Add("@Country", p_oAdd.Country);
                p.Add("@Phone", p_oAdd.Phone);
                p.Add("@Fax", p_oAdd.Fax);
                int rowsAffected = await base._cn.ExecuteAsync(sbSql.ToString(), p);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 修改顧客
        /// </summary>
        /// <param name="p_oUpd"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> UpdateCustomer(CustomerModel p_oUpd)
        {
            StringBuilder sbSql = new StringBuilder();
            DynamicParameters p = new DynamicParameters();
            try
            {
                sbSql.Append(@"UPDATE Customers 
                               SET CompanyName = @CompanyName, ContactName = @ContactName, ContactTitle = @ContactTitle,
                                   Address = @Address, City = @City, Region = @Region, PostalCode = @PostalCode,
                                    Country = @Country, Phone = @Phone, Fax = @Fax
                               WHERE CustomerID = @CustomerID,");

                p.Add("@CustomerID", p_oUpd.CustomerID);
                p.Add("@CompanyName", p_oUpd.CompanyName);
                p.Add("@ContactName", p_oUpd.ContactName);
                p.Add("@ContactTitle", p_oUpd.ContractTitle);
                p.Add("@Address", p_oUpd.Address);
                p.Add("@City", p_oUpd.City);
                p.Add("@Region", p_oUpd.Region);
                p.Add("@PostalCode", p_oUpd.PostalCode);
                p.Add("@Country", p_oUpd.Country);
                p.Add("@Phone", p_oUpd.Phone);
                p.Add("@Fax", p_oUpd.Fax);

                int rowsAffected = await base._cn.ExecuteAsync(sbSql.ToString(), p);

                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 刪除顧客
        /// </summary>
        /// <param name="p_sCustomerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> DeleteCustomer(string p_sCustomerID)
        {
            StringBuilder sbSql = new StringBuilder();
            DynamicParameters p = new DynamicParameters();
            try
            {
                sbSql.Append(@"DELETE FROM Customers WHERE CustomerID = @CustomerID;");

                p.Add("@CustomerID", p_sCustomerID);
                int rowsAffected = await base._cn.ExecuteAsync(sbSql.ToString(), p);

                return rowsAffected; 
            }
            catch (Exception ex)
            {                     
                throw new Exception(ex.Message);                
            }
        }

        /// <summary>
        /// 釋放資料庫資源
        /// </summary>
        protected void AllDispose()
        {
            base.Dispose();
        }

    }
}
