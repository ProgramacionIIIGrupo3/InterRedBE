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

        public DbSet<LugarTuristico> LugarTuristico { get; set; }
        public DbSet<Visita> Visita { get; set; }
        public DbSet<Calificacion> Calificacion { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Ruta> Ruta { get; set; }

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

            // Configuración para Ruta
            modelBuilder.Entity<Ruta>()
                .Property(r => r.TipoInicio)
                .HasConversion<string>();

            modelBuilder.Entity<Ruta>()
                .Property(r => r.TipoFin)
                .HasConversion<string>();

            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.DepartamentoInicio)
                .WithMany(d => d.RutasInicio)
                .HasForeignKey(r => r.IdEntidadInicio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.DepartamentoFin)
                .WithMany(d => d.RutasFin)
                .HasForeignKey(r => r.IdEntidadFin)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.MunicipioInicio)
                .WithMany(m => m.RutasInicio)
                .HasForeignKey(r => r.IdEntidadInicio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.MunicipioFin)
                .WithMany(m => m.RutasFin)
                .HasForeignKey(r => r.IdEntidadFin)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.LugarTuristico)
                .WithMany()
                .HasForeignKey(c => c.LugarTuristicoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Visita>()
                .HasOne(v => v.LugarTuristico)
                .WithMany()
                .HasForeignKey(v => v.LugarTuristicoId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
