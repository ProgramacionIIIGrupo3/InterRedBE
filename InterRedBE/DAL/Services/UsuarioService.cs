using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Services
{
    public class UsuarioService : IUsuarioDAO
    {
        public readonly InterRedContext _context;

        public UsuarioService(InterRedContext context)
        {
            _context = context;
        }
        public Task<OperationResponse<Usuario>> CreateOne(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Usuario>> GetAll()
        {
            var listaUsuarios = new ListaEnlazadaDoble<Usuario>();

            var usuarios = _context.Usuario.ToList();

            if (usuarios.Count == 0)
            {
                return new OperationResponse<ListaEnlazadaDoble<Usuario>>(0, "No se encontraron los usuarios", null);
            }

            foreach (var usuario in usuarios)
            {
                listaUsuarios.InsertarAlFinal(usuario);
            }

            return new OperationResponse<ListaEnlazadaDoble<Usuario>>(1,"Usuarios encontrados correctamente", listaUsuarios);

        }

        public Task<OperationResponse<Usuario>> UpdateOne(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
