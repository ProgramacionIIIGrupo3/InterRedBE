﻿using InterRedBE.UTILS.Interfaces;

namespace InterRedBE.DAL.Models
{
    public class Departamento : IIdentificable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Imagen { get; set; }
        public int? IdCabecera { get; set; }
        public string IdX { get; set; } // Nuevo Columna
        public virtual Municipio Cabecera { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; }
        public virtual ICollection<LugarTuristico> LugaresTuristicos { get; set; }
        public virtual ICollection<Ruta> RutasInicio { get; set; }
        public virtual ICollection<Ruta> RutasFin { get; set; }


        // Calcular población total del departamento
        public int PoblacionTotal => Municipios?.Sum(m => m.Poblacion) ?? 0;
    }
}
