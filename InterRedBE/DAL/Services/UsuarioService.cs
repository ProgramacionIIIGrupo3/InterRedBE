using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Services
{
    public class UsuarioService : IUsuarioBAO
    {
        public readonly InterRedContext _context;

        public UsuarioService(InterRedContext usuario)
        {
            _context = usuario;
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
            var listaUsuario = new ListaEnlazadaDoble<Usuario>();

            var usuarios = _context.Usuario.ToList();

            if(usuarios.Count == 0)
            {
                return new OperationResponse<ListaEnlazadaDoble<Usuario>>(0, "Usuarios no encontrados", null);
            }

            foreach(var usuario in usuarios)
            {
                listaUsuario.InsertarAlFinal(usuario);
            }

            return new OperationResponse<ListaEnlazadaDoble<Usuario>>(1, "Usuarios encontrados", listaUsuario);
        }

        public Task<OperationResponse<Usuario>> UpdateOne(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
