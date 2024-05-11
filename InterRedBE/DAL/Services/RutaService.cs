using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;
using Microsoft.EntityFrameworkCore;

namespace InterRedBE.DAL.Services
{
    public class RutaService : IRuta
    {

        public readonly InterRedContext _context;

        public RutaService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<(ListaCuadruple<Departamento>, Dictionary<(int, int), double>)> CargarRutasAsync()
        {
            ListaCuadruple<Departamento> lista = new ListaCuadruple<Departamento>();
            Dictionary<(int, int), double> distancias = new Dictionary<(int, int), double>();

            var rutas = await _context.Ruta.Include(r => r.DepartamentoInicio).Include(r => r.DepartamentoFin).ToListAsync();

            foreach (var ruta in rutas)
            {
                lista.AgregarNodoSiNoExiste(ruta.IdDepartamentoInicio, ruta.DepartamentoInicio);
                lista.AgregarNodoSiNoExiste(ruta.IdDepartamentoFin, ruta.DepartamentoFin);
                lista.Conectar(ruta.IdDepartamentoInicio, ruta.IdDepartamentoFin, ruta.Direccion);

                // Guardar la distancia en el diccionario para uso posterior en BAO
                distancias[(ruta.IdDepartamentoInicio, ruta.IdDepartamentoFin)] = ruta.Distancia;
            }

            return (lista, distancias);
        }
      
    }

}

