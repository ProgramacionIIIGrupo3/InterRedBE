using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Dao
{
    public interface IRuta
    {
        Task<(Grafo<IIdentificable>, Dictionary<(int, int), double>)> CargarRutasAsync();
        Task<(Grafo<IIdentificable>, Dictionary<(string, string), double>)> CargarRutasNuevoAsync();

    }
}
