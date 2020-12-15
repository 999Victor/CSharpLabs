using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class ProductsRepository : IRepository<Products>
    {
        private readonly string connectionString;
        public ProductsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Products item)
        {
            throw new NotImplementedException();
        }

        public void Delete(DateTime birthDay)
        {
            throw new NotImplementedException();
        }

        public Products Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Products> GetList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var productList = new List<Products>();

                try
                {
                    var command = new SqlCommand("GetProductList", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var products = new Products();
                            products.Id = reader.GetInt32(0);
                            products.ProducName = reader.GetString(1);
                            products.ProducNumber = reader.GetString(2);
                            products.ReorderPoint = reader.GetInt32(3);
                            products.Date = reader.GetDateTime(4);

                            productList.Add(products);
                        }
                    }
                    reader.Close();

                    return productList;
                }
                catch (Exception trouble)
                {
                    throw trouble;
                }
            }
        }

        public void Update(Products item)
        {
            throw new NotImplementedException();
        }
    }
}
