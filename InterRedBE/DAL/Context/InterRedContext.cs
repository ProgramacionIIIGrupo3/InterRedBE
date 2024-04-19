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

        }


    }
}
