using Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Products> ProductList { get; }
    }
}
