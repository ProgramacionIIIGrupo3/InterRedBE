using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class MunicipioBAOService : IMunicipioBAO
    {
        public readonly IMunicipioDAO _municipioDAO;

        public MunicipioBAOService (IMunicipioDAO municipioDAO)
        {
            _municipioDAO = municipioDAO;
        }

        public async Task<OperationResponse<Municipio>> CreateOne(Municipio obj)
        {
            try
            {
                return await _municipioDAO.CreateOne(obj);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Municipio>(0, ex.Message, null);
            }
        }

        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                return await _municipioDAO.DeleteOne(id);
            }
            catch (Exception ex)
            {
                return new OperationResponse<int>(0, ex.Message);
            }
        }

        public OperationResponse<ListaEnlazadaDoble<Municipio>> GetAll()
        {
            try
            {
                return _municipioDAO.GetAll();
            }
            catch (Exception ex) 
            {
                return new OperationResponse<ListaEnlazadaDoble<Municipio>>(0, ex.Message, null);
            }


        }

        public OperationResponse<Municipio> GetOneInt(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResponse<Municipio>> UpdateOne(Municipio obj)
        {
            try
            {
                return await _municipioDAO.UpdateOne(obj);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Municipio>(0, ex.Message, null);
            }
        }


    }
}
