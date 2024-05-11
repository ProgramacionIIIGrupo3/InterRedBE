using FluentValidation;
using InterRedBE.BAL.Bao;
//using InterRedBE.BAL.Validators;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class CalificacionBAOService : ICalificacionBAO
    {
        private readonly ICalificacionDAO _calificacionDAO;

        public CalificacionBAOService(ICalificacionDAO calificacionDAO)
        {
            _calificacionDAO = calificacionDAO;
            
        }

        public async Task<OperationResponse<Calificacion>> CreateOne(Calificacion obj)
        {
  
                return await _calificacionDAO.CreateOne(obj);
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

        public async Task<OperationResponse<Calificacion>> UpdateOne(Calificacion obj)
        {

                return await _calificacionDAO.UpdateOne(obj);
        }
    }
}