using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Bao
{
    public interface ICatalogoBAO<T>
    {
        Task<OperationResponse<T>> CreateOne(T obj);
        Task<OperationResponse<T>> UpdateOne(T obj);
        Task<OperationResponse<int>> DeleteOne(int id);
        OperationResponse<ListaEnlazadaDoble<T>> GetAll();
        Task<OperationResponse<T>> GetOneInt(int id);
        //OperationResponse<T> GetOne(string id);
        //OperationResponse<ListaEnlazadaDoble<T>> GetFiltered(string id);


    }
}
