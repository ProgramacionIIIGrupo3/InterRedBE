using FluentValidation;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class VisitaBAOService : IVisitaBAO<Visita>
    {
        private readonly IVisistaDAO<Visita> _visitaDAO;

        public VisitaBAOService(IVisistaDAO<Visita> calificacionDAO)
        {
            //_visitaDAO = visitaDAO;
        }

        public Task<OperationResponse<Visita>> CreateOne(Visita obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Visita>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<Visita>> GetOneInt(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<Visita>> UpdateOne(Visita obj)
        {
            throw new NotImplementedException();
        }
    }
}
