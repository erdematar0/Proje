using System.Text;
using ETicaret.API.Auth;
using ETicaret.Business.Extensions;
using ETicaret.Business.Validation;
using ETicaret.DataAccess.Context;
using ETicaret.Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    opt.Filters.Add<FluentValidationFilter>(FluentValidationFilter.FilterOrder);
});
builder.Services.AddBusinessLayer(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddIdentityCore<AppUser>(opt => opt.Lockout.MaxFailedAccessAttempts = 5)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager();

var jwt = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
          ?? throw new InvalidOperationException("appsettings.json içinde 'Jwt' bölümü eksik.");
if (Encoding.UTF8.GetByteCount(jwt.Key) < 32)
    throw new InvalidOperationException("Jwt:Key en az 32 byte olmalı (HMAC-SHA256).");
if (!builder.Environment.IsDevelopment() && jwt.Key.StartsWith(JwtOptions.DevKeyPrefix))
    throw new InvalidOperationException("Örnek Jwt:Key canlı ortamda kullanılamaz. Ortam değişkeni Jwt__Key ile gerçek anahtarı verin.");

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.MapInboundClaims = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = JwtTokenService.CreateKey(jwt.Key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            NameClaimType = JwtTokenService.NameClaim,
            RoleClaimType = JwtTokenService.RoleClaim
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "POST /api/auth/login ile aldığınız token'ı yapıştırın."
    });
    c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", doc)] = []
    });
});

builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
