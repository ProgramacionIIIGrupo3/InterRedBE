using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Bao
{
    public interface ILugarTuristicoBAO : ICatalogoBAO<LugarTuristico>
    {
        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetTop10Visitas();
        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetTop10ByRating();
        
    }
}
