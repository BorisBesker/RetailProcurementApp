using Infrastructure.Repository;
using ServiceLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ServiceLayer.ServicesImplementation;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    var keybytes = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:key"]);
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keybytes),
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<RetailProcurementContext>(options => options.UseSqlServer(builder.Configuration["Data:DefaultConnection:ConnectionString"]));

builder.Services.AddScoped<IUnitOfWork, UnitOfwork>();
builder.Services.AddScoped<IStoreItemService, StoreItemService>();
builder.Services.AddScoped<ISuplierService, Suplierservice>();
builder.Services.AddScoped<ISuplerItemsService, SuplerItemsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
