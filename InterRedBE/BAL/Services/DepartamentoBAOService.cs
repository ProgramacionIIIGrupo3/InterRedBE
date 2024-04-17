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

        public async Task<OperationResponse<Departamento>> CreateOne(Departamento obj)
        {
            return await _departamentoDAO.CreateOne(obj);
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

        public async Task <OperationResponse<Departamento>> GetOneInt(int id)
        {
            try
            {
                return await _departamentoDAO.GetOne( id);
            }
            catch (Exception ex)
            {
                return  new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }

        public Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }
    }
}
