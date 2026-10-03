using System.Globalization;
using ETicaret.Business.Abstract;
using ETicaret.Business.Concrete;
using ETicaret.Business.Validation;
using ETicaret.DataAccess.Context;
using ETicaret.DataAccess.Repositories.Abstract;
using ETicaret.DataAccess.Repositories.Concrete;
using ETicaret.DataAccess.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ETicaret.Business.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IPaymentService, FakePaymentService>();

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
        services.AddValidatorsFromAssemblyContaining<ProductSaveDtoValidator>();
        services.TryAddSingleton(TimeProvider.System);

        return services;
    }
}
