using ETicaret.DataAccess.Repositories.Abstract;
using ETicaret.Entities.Concrete;

namespace ETicaret.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<Category> Categories { get; }
    IRepository<Product> Products { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    IRepository<Address> Addresses { get; }
    IRepository<ContactMessage> ContactMessages { get; }
    IRepository<Review> Reviews { get; }

    Task<int> SaveChangesAsync();
}
