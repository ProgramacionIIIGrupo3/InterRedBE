using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Dao
{
    public interface IVisistaDAO<Visita>
    {
        Task<OperationResponse<Visita>> CreateOne(Visita obj);
        Task<OperationResponse<Visita>> UpdateOne(Visita obj);
        Task<OperationResponse<Visita>> GetOne(int id);

    }
}
