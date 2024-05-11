using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterRedBE.DAL.Context
{
    public class InterRedContext : DbContext
    {

        public InterRedContext(DbContextOptions<InterRedContext> options) : base(options)
        {
        }

        public DbSet<Models.LugarTuristico> LugarTuristico { get; set; }
        public DbSet<Models.Visita> Visita { get; set; }
        public DbSet<Models.Calificacion> Calificacion { get; set; }
        public DbSet<Models.Usuario> Usuario { get; set; }
        public DbSet<Models.Departamento> Departamento { get; set; }
        public DbSet<Models.Municipio> Municipio { get; set; }
        public DbSet<Models.Ruta> Ruta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración existente para Departamento y Municipio
            modelBuilder.Entity<Departamento>()
                .HasMany(d => d.Municipios)
                .WithOne(m => m.Departamento)
                .HasForeignKey(m => m.IdDepartamento);

            modelBuilder.Entity<Departamento>()
                .HasOne(d => d.Cabecera)
                .WithOne()
                .HasForeignKey<Departamento>(d => d.IdCabecera)
                .OnDelete(DeleteBehavior.Restrict);

            // Agregar configuración para LugarTuristico
            modelBuilder.Entity<LugarTuristico>()
                .HasOne(l => l.Municipio)
                .WithMany() 
                .HasForeignKey(l => l.IdMunicipio)
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<LugarTuristico>()
                .HasOne(l => l.Departamento)
                .WithMany() 
                .HasForeignKey(l => l.IdDepartamento)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuración para la relación de Ruta con DepartamentoInicio
            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.DepartamentoInicio)
                .WithMany(d => d.RutasInicio)
                .HasForeignKey(r => r.IdDepartamentoInicio)
                .OnDelete(DeleteBehavior.Restrict); 

            // Configuración para la relación de Ruta con DepartamentoFin
            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.DepartamentoFin)
                .WithMany(d => d.RutasFin)
                .HasForeignKey(r => r.IdDepartamentoFin)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
    .HasOne(c => c.LugarTuristico)
    .WithMany()
    .HasForeignKey(c => c.LugarTuristicoId) // Asegúrate de que esto coincida con la columna real en la base de datos
    .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Visita>()
                .HasOne(v => v.LugarTuristico)
                .WithMany()
                .HasForeignKey(v => v.LugarTuristicoId)
                .OnDelete(DeleteBehavior.SetNull);

        }


    }
}
