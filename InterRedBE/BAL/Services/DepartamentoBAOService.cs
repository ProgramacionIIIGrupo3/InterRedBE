using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class DepartamentoBAOService : IDepartamentoBAO
    {
        public readonly IDepartamentoDAO _departamentoDAO;

        public DepartamentoBAOService(IDepartamentoDAO departamentoDAO)
        {
            _departamentoDAO = departamentoDAO;
        }

        public Task<OperationResponse<Departamento>> CreateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Departamento>> GetAll()
        {
            try
            {
                return _departamentoDAO.GetAll();
            }
            catch (Exception ex)
            {
                return new OperationResponse<ListaEnlazadaDoble< Departamento >> (0, ex.Message, null);
            }
            
        }

        public Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }
    }
}
