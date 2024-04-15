using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class UsuarioBAOService : IUsuarioBAO
    {
        public readonly IUsuarioDAO _usuarioDAO;

        public UsuarioBAOService(IUsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
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
            try
            {
                return _usuarioDAO.GetAll();
            }
            catch (Exception ex)
            {
                return new OperationResponse<ListaEnlazadaDoble<Usuario>>(0, ex.Message, null);
            }
            
        }

        public Task<OperationResponse<Usuario>> UpdateOne(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
