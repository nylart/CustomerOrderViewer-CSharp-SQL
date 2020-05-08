using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CustomerOrderViewerV2.Models;
using Dapper;

namespace CustomerOrderViewerV2.Repository
{
    class CustomerCommand
    {
        private string _connectionString;

        public CustomerCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<CustomerModel> GetList()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            var selectStatement = "Customer_GetList";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                customers = connection.Query<CustomerModel>(selectStatement).ToList();
            }
            return customers;
        }
    }
}
