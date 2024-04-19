using InterRedBE.BAL.Bao;
using InterRedBE.BAL.Services;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using InterRedBE.DAL.DTO;

var builder = WebApplication.CreateBuilder(args);

//DAO DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMunicipioDAO, MunicipioService>();
builder.Services.AddScoped<IUsuarioDAO, UsuarioService>();
builder.Services.AddScoped<ILoginDAO, LoginService>();


////BAO DI
builder.Services.AddScoped<IUsuarioBAO, UsuarioBAOService>();
builder.Services.AddScoped<IMunicipioBAO, MunicipioBAOService>();
builder.Services.AddScoped<ILoginBAO, LoginBAOService>();


// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginDTO>());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with SQL Server
builder.Services.AddDbContext<InterRedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
