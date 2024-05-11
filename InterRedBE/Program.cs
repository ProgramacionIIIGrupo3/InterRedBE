using FluentValidation.AspNetCore;
using InterRedBE.BAL.Bao;
using InterRedBE.BAL.Services;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization; 
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using InterRedBE.AUTH.Aao;
using InterRedBE.AUTH.Service;



var builder = WebApplication.CreateBuilder(args);

// DAO DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDepartamentoDAO, DepartamentoService>();
builder.Services.AddScoped<ILugarTuristicoDAO, LugarTuristicoService>();
builder.Services.AddScoped<IMunicipioDAO, MunicipioService>();
builder.Services.AddScoped<IUsuarioDAO, UsuarioService>();
builder.Services.AddScoped<ILoginDAO, LoginService>();
builder.Services.AddScoped<IRuta, RutaService>();
builder.Services.AddScoped<IVisistaDAO, VisitaService>();
builder.Services.AddScoped<ICalificacionDAO, CalificacionService>();

// BAO DI
builder.Services.AddScoped<IDepartamentoBAO, DepartamentoBAOService>();
builder.Services.AddScoped<ILugarTuristicoBAO, LugarTuristicoBAOService>();
builder.Services.AddScoped<IUsuarioBAO, UsuarioBAOService>();
builder.Services.AddScoped<IMunicipioBAO, MunicipioBAOService>();
builder.Services.AddScoped<ILoginBAO, LoginBAOService>();
builder.Services.AddScoped<IRutaBAO, RutaBAOService>();
builder.Services.AddScoped<IVisitaBAO, VisitaBAOService>();
builder.Services.AddScoped<ICalificacionBAO, CalificacionBAOService>();


// AAO DI
builder.Services.AddScoped<IJwtAAO, JwtService>();

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginDTO>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // Manejar referencias cíclicas
        options.JsonSerializerOptions.WriteIndented = true; // Hacer la salida del JSON más legible
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        ()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration[key: "Jwt:Issuer"],
            ValidAudience = builder.Configuration[key: "Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[key: "Jwt:Key"]))
        };
    });


builder.Services.AddSwaggerGen();




// Register DbContext with SQL Server
builder.Services.AddDbContext<InterRedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var corsPolicyName = "DefaultCorsPolicy";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
