using ETicaret.DataAccess.Context;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Tests;

internal static class TestDb
{
    public const string UserId = "user-1";
    public const string OtherUserId = "user-2";

    public static AppDbContext Create(string? name = null)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(name ?? Guid.NewGuid().ToString())
            .Options;

        var db = new AppDbContext(options);
        if (db.Database.EnsureCreated())
        {
            db.Users.AddRange(
                new AppUser { Id = UserId, UserName = "ali@test.com", Email = "ali@test.com", FirstName = "Ali", LastName = "Yılmaz" },
                new AppUser { Id = OtherUserId, UserName = "ayse@test.com", Email = "ayse@test.com", FirstName = "Ayşe", LastName = "Demir" });
            db.SaveChanges();
            db.ChangeTracker.Clear();
        }
        return db;
    }

    public static UnitOfWork Uow(AppDbContext db) => new(db);
}
