using InterRedBE.DAL.DTO;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Dao
{
    public interface ICRUD<T>
    {
        Task<OperationResponse<T>> CreateOne(T obj);
        Task<OperationResponse<T>> UpdateOne(T obj);
        Task<OperationResponse<int>> DeleteOne(int id);
        OperationResponse<ListaEnlazadaDoble<T>> GetAll();
        Task<OperationResponse<T>> GetOne(int id);
        
        // Task<OperationResponse<int>> DeleteOneGUID(string id);
        // Task<OperationResponse<T>> GetOneString(string id);
        // Task<OperationResponse<ListaEnlazadaDoble<T>>> GetFiltered(string id);
    }
}
