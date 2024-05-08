using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Dao
{
    public interface IRuta
    {
        Task<(ListaCuadruple<Departamento>, Dictionary<(int, int), double>)> CargarRutasAsync();
      
    }
}
