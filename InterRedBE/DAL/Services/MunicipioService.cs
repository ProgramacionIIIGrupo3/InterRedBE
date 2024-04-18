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
            // Validación del objeto Municipio usando DataAnnotations.
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, validationContext, validationResults, true))
            {
                // Si la validación falla, se devuelve una respuesta con los errores.
                return new OperationResponse<Municipio>(0, "Validación fallida: " + string.Join(", ", validationResults.Select(s => s.ErrorMessage)), null);
            }

            // Convertir el nombre del departamento a su ID correspondiente antes de guardar el municipio.
            var departamento = await _context.Departamento.FirstOrDefaultAsync(d => d.Nombre == obj.NombreDepartamento);
            if (departamento == null)
            {
                // Si no se encuentra el departamento, se devuelve una respuesta con un mensaje de error.
                return new OperationResponse<Municipio>(0, "Departamento no encontrado.", null);
            }
            obj.IdDepartamento = departamento.Id;

            try
            {
                // Agregar el municipio al contexto y guardar los cambios en la base de datos.
                _context.Municipio.Add(obj);
                await _context.SaveChangesAsync();
                // Devolver una respuesta exitosa con el municipio creado.
                return new OperationResponse<Municipio>(1, "Municipio creado con éxito.", obj);
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante el guardado, se devuelve una respuesta con el mensaje de error.
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
            // Validación del objeto Municipio usando DataAnnotations.
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, validationContext, validationResults, true))
            {
                // Si la validación falla, se devuelve una respuesta con los errores.
                return new OperationResponse<Municipio>(0, "Validación fallida: " + string.Join(", ", validationResults.Select(s => s.ErrorMessage)), null);
            }

            // Convertir el nombre del departamento a su ID correspondiente antes de actualizar el municipio.
            var departamento = await _context.Departamento.FirstOrDefaultAsync(d => d.Nombre == obj.NombreDepartamento);
            if (departamento == null)
            {
                // Si no se encuentra el departamento, se devuelve una respuesta con un mensaje de error.
                return new OperationResponse<Municipio>(0, "Departamento no encontrado.", null);
            }
            obj.IdDepartamento = departamento.Id;

            try
            {
                // Buscar el municipio existente en la base de datos por su ID.
                var municipioExistente = await _context.Municipio.FindAsync(obj.Id);
                if (municipioExistente == null)
                {
                    // Si el municipio no se encuentra, se devuelve una respuesta con un mensaje de error.
                    return new OperationResponse<Municipio>(0, "Municipio no encontrado.", null);
                }

                // Actualizar los datos del municipio en el contexto y guardar los cambios en la base de datos.
                _context.Entry(municipioExistente).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                // Devolver una respuesta exitosa con el municipio actualizado.
                return new OperationResponse<Municipio>(1, "Municipio actualizado con éxito.", obj);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Si ocurre un error de concurrencia durante la actualización, se devuelve una respuesta con el mensaje de error.
                if (!_context.Municipio.Any(e => e.Id == obj.Id))
                {
                    return new OperationResponse<Municipio>(0, "Municipio no encontrado.", null);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la actualización, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<Municipio>(0, ex.Message, null);
            }
        }

        // Método asíncrono para obtener un municipio por su ID.
        public async Task<OperationResponse<Municipio>> GetOneById(int id)
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

