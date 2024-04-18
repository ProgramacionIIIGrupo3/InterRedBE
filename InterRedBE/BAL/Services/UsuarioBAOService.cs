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

        public async Task<OperationResponse<Usuario>> CreateOne(Usuario obj)
        {
            try
            {
                return await _usuarioDAO.CreateOne(obj);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Usuario>(0, ex.Message, null);
            }
        }

        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                return await _usuarioDAO.DeleteOne(id);
            }
            catch (Exception ex)
            {
                return new OperationResponse<int>(0, ex.Message);
            }
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

        public OperationResponse<Usuario> GetOneInt(int id)
        {
            //No toques :D
            throw new NotImplementedException();
        }

        public async Task<OperationResponse<Usuario>> UpdateOne(Usuario obj)
        {
            try
            {
                return await _usuarioDAO.UpdateOne(obj);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Usuario>(0, ex.Message, null);
            }
        }

     
    }
}
