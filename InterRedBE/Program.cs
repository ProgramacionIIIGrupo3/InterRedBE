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
using InterRedBE.AUTH.Aao;
using InterRedBE.AUTH.Service;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
var configuration = builder.Configuration;

// Add MemoryCache service
builder.Services.AddMemoryCache();

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

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrador"));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InterRed API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
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
    }});
});

// Register DbContext with SQL Server
builder.Services.AddDbContextFactory<InterRedContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


var corsPolicyName = "DefaultCorsPolicy";
var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
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
