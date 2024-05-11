using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Bao
{
    public interface ICalificacionBAO
    {
        Task<OperationResponse<Calificacion>> CreateOne(Calificacion obj);
        Task<OperationResponse<Calificacion>> UpdateOne(Calificacion obj);
        Task<OperationResponse<int>> DeleteOne(int id);
        OperationResponse<ListaEnlazadaDoble<Calificacion>> GetAll();
        Task<OperationResponse<Calificacion>> GetOneInt(int id);

    }
}
