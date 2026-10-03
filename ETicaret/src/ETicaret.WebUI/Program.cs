using ETicaret.Business.Extensions;
using ETicaret.Business.Validation;
using ETicaret.DataAccess.Context;
using ETicaret.Entities.Concrete;
using System.Threading.RateLimiting;
using ETicaret.WebUI.Controllers;
using ETicaret.WebUI.Data;
using ETicaret.WebUI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opt =>
{
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    opt.Filters.Add<FluentValidationFilter>(FluentValidationFilter.FilterOrder);

    opt.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Bu alan zorunludur.");
    opt.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, _) => $"'{value}' geçerli bir değer değil.");
});

builder.Services.AddBusinessLayer(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredUniqueChars = RuleBuilderExtensions.MinUniquePasswordChars;
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.ExpireTimeSpan = TimeSpan.FromDays(7);
    opt.SlidingExpiration = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromHours(2);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICartService, SessionCartService>();

builder.Services.AddScoped<IImageStorage, LocalImageStorage>();

builder.Services.AddRateLimiter(opt =>
{
    opt.AddPolicy(ContactController.RateLimitPolicy, ctx => RateLimitPartition.GetFixedWindowLimiter(
        ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 5, Window = TimeSpan.FromMinutes(10) }));

    opt.OnRejected = (ctx, _) =>
    {
        var tempData = ctx.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>().GetTempData(ctx.HttpContext);
        tempData["Error"] = "Çok fazla mesaj gönderdin. Lütfen birkaç dakika sonra tekrar dene.";
        tempData.Save();
        ctx.HttpContext.Response.Redirect("/Contact");
        return ValueTask.CompletedTask;
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRateLimiter();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await DbSeeder.SeedAsync(app.Services, app.Configuration);

app.Run();
