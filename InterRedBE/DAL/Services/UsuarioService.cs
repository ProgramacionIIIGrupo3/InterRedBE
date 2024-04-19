using InterRedBE.DAL.Context;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using InterRedBE.DAL.Dao;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Services
{
    // Definición del servicio que implementa la interfaz IUsuarioDAO.
    public class UsuarioService : IUsuarioDAO
    {
        // Contexto de la base de datos inyectado a través del constructor.
        private readonly InterRedContext _context;

        public UsuarioService(InterRedContext context)
        {
            _context = context;
        }

        // Método asíncrono para crear un nuevo usuario.
        public async Task<OperationResponse<Usuario>> CreateOne(Usuario obj)
        {
    
            try
            {
                // Si la validación es exitosa, se agrega el usuario al contexto y se guarda en la base de datos.
                _context.Usuario.Add(obj);
                await _context.SaveChangesAsync();
                // Se devuelve una respuesta exitosa con el usuario creado.
                return new OperationResponse<Usuario>(1, "Usuario creado con éxito.", obj);
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante el guardado, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<Usuario>(0, ex.Message, null);
            }
        }

        // Método asíncrono para eliminar un usuario existente por su ID.
        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                // Se busca el usuario en la base de datos por su ID.
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    // Si el usuario no se encuentra, se devuelve una respuesta con un mensaje de error.
                    return new OperationResponse<int>(0, "Usuario no encontrado.", 0);
                }

                // Si el usuario existe, se elimina del contexto y se guarda el cambio en la base de datos.
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
                // Se devuelve una respuesta exitosa con el ID del usuario eliminado.
                return new OperationResponse<int>(1, "Usuario eliminado con éxito.", id);
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la eliminación, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<int>(0, ex.Message, 0);
            }
        }

        // Método para obtener todos los usuarios existentes.
        public OperationResponse<ListaEnlazadaDoble<Usuario>> GetAll()
        {
            try
            {
                // Se obtienen todos los usuarios del contexto de la base de datos.
                var usuarios = _context.Usuario.ToList();
                var listaUsuarios = new ListaEnlazadaDoble<Usuario>();

                // Se insertan los usuarios en una lista doblemente enlazada.
                foreach (var usuario in usuarios)
                {
                    listaUsuarios.InsertarAlFinal(usuario);
                }

                // Se devuelve una respuesta exitosa con la lista de usuarios.
                return new OperationResponse<ListaEnlazadaDoble<Usuario>>(1, "Usuarios encontrados", listaUsuarios);
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la obtención, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<ListaEnlazadaDoble<Usuario>>(0, ex.Message, null);
            }
        }

        public Task<OperationResponse<Usuario>> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        // Método asíncrono para actualizar un usuario existente.
        public async Task<OperationResponse<Usuario>> UpdateOne(Usuario obj)
        {
            try
            {
                // Se busca el usuario en la base de datos por su ID.
                var usuarioExistente = _context.Usuario.FirstOrDefault(d => d.Id == obj.Id);
                if (usuarioExistente == null)
                {
                    // Si el usuario no se encuentra, se devuelve una respuesta con un mensaje de error.
                    return new OperationResponse<Usuario>(0, "Usuario no encontrado.", null);
                }

                // Si el usuario existe, se actualizan sus datos en el contexto y se guarda el cambio en la base de datos.
                _context.Entry(usuarioExistente).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                // Se devuelve una respuesta exitosa con el usuario actualizado.
                return new OperationResponse<Usuario>(1, "Usuario actualizado con éxito.", obj);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Si ocurre un error de concurrencia durante la actualización, se devuelve una respuesta con el mensaje de error.
                if (!_context.Usuario.Any(e => e.Id == obj.Id))
                {
                    return new OperationResponse<Usuario>(0, "Usuario no encontrado.", null);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante la actualización, se devuelve una respuesta con el mensaje de error.
                return new OperationResponse<Usuario>(0, ex.Message, null);
            }


        }
    }
}
