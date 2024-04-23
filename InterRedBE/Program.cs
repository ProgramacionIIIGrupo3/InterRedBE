using FluentValidation.AspNetCore;
using InterRedBE.BAL.Bao;
using InterRedBE.BAL.Services;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DAO DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDepartamentoDAO, DepartamentoService>();
builder.Services.AddScoped<ILugarTuristicoDAO, LugarTuristicoService>();
builder.Services.AddScoped<IMunicipioDAO, MunicipioService>();
builder.Services.AddScoped<IUsuarioDAO, UsuarioService>();
builder.Services.AddScoped<ILoginDAO, LoginService>();

// BAO DI
builder.Services.AddScoped<IDepartamentoBAO, DepartamentoBAOService>();
builder.Services.AddScoped<ILugarTuristicoBAO, LugarTuristicoBAOService>();
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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

