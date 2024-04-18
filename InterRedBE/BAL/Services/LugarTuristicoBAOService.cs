using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class LugarTuristicoBAOService : ILugarTuristicoBAO
    {
        public readonly ILugarTuristicoDAO _lugarTuristicoDAO;

        public LugarTuristicoBAOService(ILugarTuristicoDAO lugarTuristicoDAO)
        {
            _lugarTuristicoDAO = lugarTuristicoDAO;
        }

        public Task<OperationResponse<LugarTuristico>> CreateOne(LugarTuristico obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetAll()
        {
            try
            {
                return _lugarTuristicoDAO.GetAll();
            }
            catch (Exception ex)
            {
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(0, ex.Message, null);
            }
        }

        public Task<OperationResponse<LugarTuristico>> GetOneInt(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<LugarTuristico>> UpdateOne(LugarTuristico obj)
        {
            throw new NotImplementedException();
        }
    }
}
