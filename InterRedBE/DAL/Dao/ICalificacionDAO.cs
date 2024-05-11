using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Dao
{
    public interface ICalificacionDAO
    { 
            Task<OperationResponse<Calificacion>> CreateOne(Calificacion obj);
            Task<OperationResponse<Calificacion>> UpdateOne(Calificacion obj);
            Task<OperationResponse<Calificacion>> GetOne(int id);
        
        
    }
}
