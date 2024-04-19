using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Dao
{
    public interface ILugarTuristicoDAO : ICRUD<LugarTuristico>
    {
        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetTop10Visitas();
        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetTop10ByRating();
        
    }
}
