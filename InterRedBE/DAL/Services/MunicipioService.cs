using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;

namespace InterRedBE.DAL.Services
{
    // Clase de servicio que implementa la interfaz IMunicipioDAO para operaciones de base de datos con la entidad Municipio.
    public class MunicipioService : IMunicipioDAO
    {
        // Contexto de la base de datos proporcionado por Entity Framework Core.
        private readonly InterRedContext _context;

        // Constructor que inyecta el contexto de la base de datos en el servicio.
        public MunicipioService(InterRedContext context)
        {
            _context = context;
        }

        // Método asíncrono para crear un nuevo municipio en la base de datos.
        public async Task<OperationResponse<Municipio>> CreateOne(Municipio obj)
        {
            try
            {
                _context.Municipio.Add(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Municipio>(1, "Felicidades Municipio encontrado", obj);

            }
            catch (Exception ex)
            {
                return new OperationResponse<Municipio>(0, ex.Message, null);
            }
           


        }

        // Método asíncrono para eliminar un municipio existente por su ID.
        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                // Buscar el municipio en la base de datos por su ID.
                var municipio = await _context.Municipio.FindAsync(id);
                if (municipio == null)
                {
                    // Si el municipio no se encuentra, se devuelve una respuesta con un mensaje de error.
                    return new OperationResponse<int>(0, "Municipio no encontrado.", 0);
                }

                // Eliminar el municipio del contexto y guardar los cambios en la base de datos.
                _context.Municipio.Remove(municipio);
                await _context.SaveChangesAsync();
                // Devolver una respuesta exitosa con el ID del municipio eliminado.
                return new OperationResponse<int>(1, "Municipio eliminado con éxito.", id);
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la eliminación, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<int>(0, ex.Message, 0);
            }
        }

        // Método para obtener todos los municipios existentes en la base de datos.
        public OperationResponse<ListaEnlazadaDoble<Municipio>> GetAll()
        {
            var listaMunicipios = new ListaEnlazadaDoble<Municipio>();

            // Recuperar todos los municipios del contexto de la base de datos.
            var municipios = _context.Municipio.ToList();

            if (municipios.Count == 0)
            {
                // Si no hay municipios, se devuelve una respuesta indicando que no se encontraron.
                return new OperationResponse<ListaEnlazadaDoble<Municipio>>(0, "Municipios no encontrados", null);
            }

            // Insertar cada municipio en una lista doblemente enlazada.
            foreach (var municipio in municipios)
            {
                listaMunicipios.InsertarAlFinal(municipio);
            }

            // Devolver una respuesta exitosa con la lista de municipios.
            return new OperationResponse<ListaEnlazadaDoble<Municipio>>(1, "Municipios encontrados", listaMunicipios);
        }

        // Método asíncrono para actualizar un municipio existente en la base de datos.
        public async Task<OperationResponse<Municipio>> UpdateOne(Municipio obj)
        {
            try
            {
                var municipioActualizar = _context.Municipio.FirstOrDefault(d=>d.Id==obj.Id);
                if(municipioActualizar == null)
                {
                   return new OperationResponse<Municipio>(0, "No se encontro", null);

                }
                municipioActualizar.Nombre = obj.Nombre;
                municipioActualizar.Descripcion = obj.Descripcion;
                municipioActualizar.Poblacion=obj.Poblacion;
                municipioActualizar.IdDepartamento = obj.IdDepartamento;

                _context.Municipio.Update(municipioActualizar);
                await _context.SaveChangesAsync();
                return new OperationResponse<Municipio>(1, "Si se actualizo", municipioActualizar);


            }
            catch (Exception ex)
            {
                return new OperationResponse<Municipio>(0, ex.Message, null);
            }

        }

        // Método asíncrono para obtener un municipio por su ID.
        public async Task<OperationResponse<Municipio>> GetOne(int id)
        {
            try
            {
                // Buscar el municipio en la base de datos por su ID.
                var municipio = await _context.Municipio.FindAsync(id);
                if (municipio == null)
                {
                    // Si el municipio no se encuentra, se devuelve una respuesta con un mensaje de error.
                    return new OperationResponse<Municipio>(0, "Municipio no encontrado.", null);
                }

                // Convertir el ID del departamento a su nombre correspondiente antes de devolver el municipio.
               //unicipio.NombreDepartamento = _context.Departamento.Where(d => d.Id == municipio.IdDepartamento).Select(d => d.Nombre).FirstOrDefault();
                // Devolver una respuesta exitosa con el municipio encontrado.
                return new OperationResponse<Municipio>(1, "Municipio encontrado.", municipio);
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la búsqueda, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<Municipio>(0, ex.Message, null);
            }
        }
    }
}

