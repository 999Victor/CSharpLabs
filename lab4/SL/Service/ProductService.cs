using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Models;

namespace SL.Service
{
    public class ProductService
    {
        private IUnitOfWork Database { get; set; }

        public ProductService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<Products> GetListOfEmployees()
        {
            return Database.ProductList.GetList();
        }
    }
}
