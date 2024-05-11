using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.BAL.Services
{
    public class VisitaBAOService : IVisitaBAO
    {
        private readonly IVisistaDAO _visitaDAO;

        public VisitaBAOService(IVisistaDAO visitaDAO)
        {
            _visitaDAO = visitaDAO;
        }

        public async Task<OperationResponse<Visita>> CreateOne(Visita obj)
        {
            return await _visitaDAO.CreateOne(obj);
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Visita>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResponse<Visita>> GetOneInt(int id)
        {
            return await _visitaDAO.GetOne(id);
        }

        public async Task<OperationResponse<Visita>> UpdateOne(Visita obj)
        {
            return await _visitaDAO.UpdateOne(obj);
        }
    }
}