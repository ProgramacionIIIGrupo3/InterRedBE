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

        public Task<OperationResponse<Municipio>> CreateOne(Municipio obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
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

        public Task<OperationResponse<Municipio>> UpdateOne(Municipio obj)
        {
            throw new NotImplementedException();
        }
    }
}
