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

    }
}
