using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
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


        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                return await _departamentoDAO.DeleteOne(id);
            }
            catch (Exception ex)
            {
                return new OperationResponse<int>(0, ex.Message, 0);
            }
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

        public async Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            try
            {
                return await _departamentoDAO.UpdateOne(obj);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }

    }
}
