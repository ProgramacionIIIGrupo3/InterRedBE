using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InterRedBE.DAL.Services
{
    // Clase que implementa la interfaz ILugarTuristicoDAO para interactuar con la base de datos de lugares turísticos
    public class LugarTuristicoService : ILugarTuristicoDAO
    {
        // Contexto de la base de datos
        public readonly InterRedContext _context;

        // Constructor que recibe el contexto de la base de datos
        public LugarTuristicoService(InterRedContext context)
        {
            _context = context;
        }

        // Método para crear un nuevo lugar turístico 
        public async Task<OperationResponse<LugarTuristico>> CreateOne(LugarTuristico obj)
        {
            try
            {
                // Agregar el nuevo lugar turístico al contexto y guardar los cambios en la base de datos
                _context.LugarTuristico.Add(obj);
                await _context.SaveChangesAsync();
                // Devolver una respuesta exitosa con el lugar turístico creado
                return new OperationResponse<LugarTuristico>(1, "Lugar Turistico creado exitosamente", obj);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la creación y devolver una respuesta de error
                return new OperationResponse<LugarTuristico>(0, ex.Message, null);
            }
        }

        // Método para eliminar un lugar turístico 
        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                // Buscar el lugar turístico por su ID
                var lugarTuristico = await _context.LugarTuristico.FindAsync(id);

                // Verificar si el lugar turístico existe
                if (lugarTuristico == null)
                {
                    // Si no existe, devolver un mensaje de error
                    return new OperationResponse<int>(0, "El Lugar Turistico no existe", 0);
                }

                // Eliminar el lugar turístico del contexto y guardar los cambios en la base de datos
                _context.LugarTuristico.Remove(lugarTuristico);
                await _context.SaveChangesAsync();

                // Devolver una respuesta exitosa con el ID del lugar turístico eliminado
                return new OperationResponse<int>(1, "Lugar Turistico eliminado correctamente", id);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la eliminación y devolver una respuesta de error
                return new OperationResponse<int>(0, ex.Message, 0);
            }
        }

        // Método para obtener todos los lugares turísticos 
        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetAll()
        {
            try
            {
                // Obtener todos los lugares turísticos de la base de datos
                var lugaresTuristicos = _context.LugarTuristico.ToList();

                // Verificar si se encontraron lugares turísticos
                if (lugaresTuristicos.Count == 0)
                {
                    // Si no se encontraron, devolver un mensaje de error
                    return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(0, "No se encontraron los Lugares Turisticos", null);
                }

                // Crear una lista enlazada doble para almacenar los lugares turísticos
                var listaLugaresTuristicos = new ListaEnlazadaDoble<LugarTuristico>();

                // Insertar cada lugar turístico en la lista enlazada
                foreach (var lugarTuristico in lugaresTuristicos)
                {
                    listaLugaresTuristicos.InsertarAlFinal(lugarTuristico);
                }

                // Devolver una respuesta exitosa con la lista de lugares turísticos encontrados
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(1, "Lugares Turisticos Encontrados Correctamente", listaLugaresTuristicos);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la obtención de los lugares turísticos y devolver una respuesta de error
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(0, ex.Message, null);
            }
        }

        // Método para obtener un lugar turístico de la base de datos por su ID
        public async Task<OperationResponse<LugarTuristico>> GetOne(int id)
        {
            try
            {
                // Buscar el lugar turístico por su ID
                var lugarTuristico = _context.LugarTuristico.FirstOrDefault(ts => ts.Id == id);

                // Verificar si se encontró el lugar turístico
                if (lugarTuristico != null)
                {
                    // Si se encontró, devolver una respuesta exitosa con el lugar turístico encontrado
                    return new OperationResponse<LugarTuristico>(1, "Lugar Turistico Encontrado Correctamente", lugarTuristico);
                }
                else
                {
                    // Si no se encontró, devolver un mensaje de error
                    return new OperationResponse<LugarTuristico>(0, "Lugar Turistico no encontrado", null);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la obtención del lugar turístico y devolver una respuesta de error
                return new OperationResponse<LugarTuristico>(0, ex.Message, null);
            }
        }

        // Método para actualizar un lugar turístico en la base de datos
        public async Task<OperationResponse<LugarTuristico>> UpdateOne(LugarTuristico obj)
        {
            try
            {
                // Buscar el lugar turístico existente por su ID
                var lugarTuristicoExistente = _context.LugarTuristico.FirstOrDefault(d => d.Id == obj.Id);

                // Verificar si el lugar turístico existe
                if (lugarTuristicoExistente == null)
                {
                    // Si no existe, devolver un mensaje de error
                    return new OperationResponse<LugarTuristico>(0, "El Lugar Turístico no existe", null);
                }

                // Actualizar los datos del lugar turístico existente con los datos del objeto proporcionado
                lugarTuristicoExistente.Nombre = obj.Nombre;
                lugarTuristicoExistente.Descripcion = obj.Descripcion;
                lugarTuristicoExistente.Imagen = obj.Imagen;
                lugarTuristicoExistente.IdMunicipio = obj.IdMunicipio;
                lugarTuristicoExistente.IdDepartamento = obj.IdDepartamento;

                // Actualizar el lugar turístico en el contexto y guardar los cambios en la base de datos
                _context.LugarTuristico.Update(lugarTuristicoExistente);
                await _context.SaveChangesAsync();

                // Devolver una respuesta exitosa con el lugar turístico actualizado
                return new OperationResponse<LugarTuristico>(1, "Lugar Turistico actualizado exitosamente", lugarTuristicoExistente);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la actualización y devolver una respuesta de error
                return new OperationResponse<LugarTuristico>(0, ex.Message, null);
            }
        }

        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetTop10Visitas()
        {
            try
            {
                // Obtener todos los lugares turísticos de la base de datos, incluyendo las visitas relacionadas
                var lugaresTuristicos = _context.LugarTuristico.Include(lt => lt.Visitas).ToList();

                // Ordenar los lugares turísticos por la cantidad de visitas en orden descendente
                var lugaresOrdenados = lugaresTuristicos.OrderByDescending(lt => lt.Visitas.Count).ToList();

                
                var top10 = new ListaEnlazadaDoble<LugarTuristico>();

                // Insertar cada lugar turístico 
                foreach (var lugarTuristico in lugaresOrdenados.Take(10))
                {
                    top10.InsertarAlFinal(lugarTuristico);
                }

                // Devolver una respuesta exitosa con los 10 lugares turísticos con más visitas
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(1, "Top 10 Lugares Turísticos con Más Visitas", top10);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la obtención de los lugares turísticos y devolver una respuesta de error
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(0, ex.Message, null);
            }
        }
        public OperationResponse<ListaEnlazadaDoble<LugarTuristicoConPromedioDTO>> GetTop10ByRating()
        {
            try
            {
                // Obtener todos los lugares turísticos de la base de datos, incluyendo las calificaciones relacionadas
                var lugaresTuristicos = _context.LugarTuristico.Include(lt => lt.Calificaciones).ToList();

                // Ordenar los lugares turísticos por la calificación promedio en orden descendente y mapear a DTO
                var lugaresConPromedio = lugaresTuristicos.Select(lt => new LugarTuristicoConPromedioDTO
                {
                    LugarTuristico = lt,
                    PromedioCalificaciones = lt.Calificaciones.Any() ? lt.Calificaciones.Average(c => Convert.ToDouble(c.Puntuacion)) : 0
                })
                .OrderByDescending(dto => dto.PromedioCalificaciones)
                .ToList();

                // Crear una nueva instancia de ListaEnlazadaDoble<LugarTuristicoConPromedioDTO>
                var top10 = new ListaEnlazadaDoble<LugarTuristicoConPromedioDTO>();

                // Insertar los primeros 10 lugares turísticos en la lista enlazada doble
                foreach (var dto in lugaresConPromedio.Take(10))
                {
                    top10.InsertarAlFinal(dto);
                }

                // Devolver una respuesta exitosa con los 10 lugares turísticos mejor calificados
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristicoConPromedioDTO>>(1, "Top 10 Lugares Turísticos Mejor Calificados", top10);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la obtención de los lugares turísticos y devolver una respuesta de error
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristicoConPromedioDTO>>(0, ex.Message, null);
            }
        }

    }
}
