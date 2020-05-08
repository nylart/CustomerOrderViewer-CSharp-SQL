using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CustomerOrderViewerV2.Models;
using Dapper;

namespace CustomerOrderViewerV2.Repository
{
    class ItemCommand
    {
        private string _connectionString;

        public ItemCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<ItemModel> GetList()
        {
            List<ItemModel> items = new List<ItemModel>();
            var selectStatement = "Item_GetList";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                items = connection.Query<ItemModel>(selectStatement).ToList();
            }
            return items;
        }
    }
}
