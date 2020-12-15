using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public string ConnectionString { get; set; }
        private ProductsRepository productsRepository;

        public UnitOfWork(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IRepository<Products> ProductList
        {
            get
            {
                if (productsRepository == null)
                    productsRepository = new ProductsRepository(ConnectionString);
                return productsRepository;
            }
        }
    }
}
