using BusBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Repositories;
using BusBookingSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<CoordinateService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bus Booking System API v1",
        Description = "An ASP.NET Core Web API for bus booking system.",
        Contact = new OpenApiContact
        {
            Name = "HARSHA VARHDAN V",
            Url = new Uri("https://harsha.engineer")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authentication",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<BookingService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
