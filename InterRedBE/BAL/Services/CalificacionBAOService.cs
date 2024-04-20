using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;


namespace InterRedBE.BAL.Services
{
    public class CalificacionBAOService : ICalificacionBAO<Calificacion>
    {
        public readonly ICalificacionDAO<Calificacion> _calificacionDAO;

        public CalificacionBAOService(ICalificacionDAO<Calificacion> calificacionDAO)
        {
           _calificacionDAO = calificacionDAO;
        }

        public async Task<OperationResponse<Calificacion>> CreateOne(Calificacion obj)
        {
            try
            {
                return await _calificacionDAO.CreateOne(obj);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Calificacion>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<Calificacion>> GetOneInt(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<Calificacion>> UpdateOne(Calificacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
