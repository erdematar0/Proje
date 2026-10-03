using ETicaret.DataAccess.Context;
using ETicaret.DataAccess.Repositories.Abstract;
using ETicaret.DataAccess.Repositories.Concrete;
using ETicaret.Entities.Concrete;

namespace ETicaret.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Categories = new Repository<Category>(context);
        Products = new Repository<Product>(context);
        Orders = new Repository<Order>(context);
        OrderItems = new Repository<OrderItem>(context);
        Addresses = new Repository<Address>(context);
        ContactMessages = new Repository<ContactMessage>(context);
        Reviews = new Repository<Review>(context);
    }

    public IRepository<Category> Categories { get; }
    public IRepository<Product> Products { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<OrderItem> OrderItems { get; }
    public IRepository<Address> Addresses { get; }
    public IRepository<ContactMessage> ContactMessages { get; }
    public IRepository<Review> Reviews { get; }

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
