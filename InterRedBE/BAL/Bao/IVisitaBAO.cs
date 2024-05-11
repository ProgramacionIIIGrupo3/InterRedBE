using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Bao
{
    public interface IVisitaBAO
    {
        Task<OperationResponse<Visita>> CreateOne(Visita obj);
        Task<OperationResponse<Visita>> UpdateOne(Visita obj);
        Task<OperationResponse<int>> DeleteOne(int id);
        OperationResponse<ListaEnlazadaDoble<Visita>> GetAll();
        Task<OperationResponse<Visita>> GetOneInt(int id);

    }
}
